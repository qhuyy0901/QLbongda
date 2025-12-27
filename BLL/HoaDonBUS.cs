using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class HoaDonBUS
    {
        private Model1 db = new Model1();

        // ===== TỰ ĐỘNG SINH MÃ HÓA ĐƠN =====
        private string GenerateMaHoaDon()
        {
            try
            {
                int count = db.HoaDons.Count();
                string maHD = $"HD{(count + 1):D3}";

                while (db.HoaDons.Any(x => x.MaHD == maHD))
                {
                    count++;
                    maHD = $"HD{count:D3}";
                }

                return maHD;
            }
            catch
            {
                return "HD" + DateTime.Now.Ticks.ToString().Substring(0, 10);
            }
        }

        // ===== HÀM THANH TOÁN (TRANSACTION) =====
        public bool ThanhToan(HoaDon hd, List<CT_HoaDon_DichVu> listChiTiet)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // ===== VALIDATE DỮ LIỆU =====
                    if (hd == null)
                    {
                        System.Diagnostics.Debug.WriteLine("❌ HoaDon không được null");
                        return false;
                    }

                    // ===== SINH MÃ HÓA ĐƠN TỰ ĐỘNG =====
                    if (string.IsNullOrEmpty(hd.MaHD))
                    {
                        hd.MaHD = GenerateMaHoaDon();
                    }

                    // ===== ĐẢM BẢO CÓ NGÀY GIỜ THANH TOÁN =====
                    if (!hd.ThoiGianThanhToan.HasValue)
                    {
                        hd.ThoiGianThanhToan = DateTime.Now;
                    }

                    // ===== KIỂM TRA MALÍCH CÓ HỢP LỆ KHÔNG =====
                    // Nếu MaLich rỗng hoặc "KVL" (khách vãng lai), để NULL thay vì "KVL"
                    // vì "KVL" không tồn tại trong bảng LichDat
                    if (string.IsNullOrEmpty(hd.MaLich) || hd.MaLich == "KVL")
                    {
                        // ✅ ĐỔI: Thay vì để "KVL", hãy để NULL
                        // Nếu MaLich nullable trong Database, điều này sẽ giải quyết vấn đề FK
                        hd.MaLich = null;
                    }
                    else
                    {
                        // ✅ KIỂM TRA: Nếu MaLich không NULL, phải tồn tại trong bảng LichDat
                        var lichDat = db.LichDats.FirstOrDefault(x => x.MaLich == hd.MaLich);
                        if (lichDat == null)
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Lịch đặt {hd.MaLich} không tồn tại trong database");
                            return false;
                        }
                    }

                    // 1️⃣ LƯU HÓA ĐƠN TRƯỚC
                    db.HoaDons.Add(hd);
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine($"✅ Lưu hóa đơn thành công: {hd.MaHD}");

                    // 2️⃣ LƯU CHI TIẾT HÓA ĐƠN (NẾU CÓ)
                    if (listChiTiet != null && listChiTiet.Count > 0)
                    {
                        int stt = 1;
                        foreach (var item in listChiTiet)
                        {
                            // ===== SINH MÃ CHI TIẾT =====
                            if (string.IsNullOrEmpty(item.MaCT))
                            {
                                item.MaCT = $"CT{hd.MaHD.Substring(2)}{stt:D2}";
                                stt++;
                            }

                            item.MaHD = hd.MaHD;

                            // ===== ĐẢM BẢO CÓ THÀNH TIỀN =====
                            if (!item.ThanhTien.HasValue || item.ThanhTien == 0)
                            {
                                if (!string.IsNullOrEmpty(item.MaDV))
                                {
                                    var dichVu = db.DichVus.FirstOrDefault(x => x.MaDV == item.MaDV);
                                    if (dichVu != null)
                                    {
                                        item.ThanhTien = (dichVu.DonGia ?? 0) * (item.SoLuong ?? 1);
                                    }
                                    else
                                    {
                                        item.ThanhTien = (item.DichVu?.DonGia ?? 0) * (item.SoLuong ?? 1);
                                    }
                                }
                                else
                                {
                                    item.ThanhTien = 0;
                                }
                            }

                            // ===== DETACH ENTITY TRƯỚC KHI ADD ĐỂ TRÁNH CONFLICT =====
                            var existingItem = db.CT_HoaDon_DichVu.Local.FirstOrDefault(x => x.MaCT == item.MaCT);
                            if (existingItem != null)
                            {
                                db.Entry(existingItem).State = System.Data.Entity.EntityState.Detached;
                            }

                            db.CT_HoaDon_DichVu.Add(item);
                        }
                        db.SaveChanges();
                        System.Diagnostics.Debug.WriteLine($"✅ Lưu {listChiTiet.Count} chi tiết dịch vụ thành công");
                    }

                    // 3️⃣ CẬP NHẬT TRẠNG THÁI LỊCH ĐẶT (CHỈ NẾU CÓ MALÍCH HỢP LỆ)
                    if (!string.IsNullOrEmpty(hd.MaLich))
                    {
                        var lich = db.LichDats.FirstOrDefault(x => x.MaLich == hd.MaLich);
                        if (lich != null)
                        {
                            lich.TrangThai = "Hoàn Thành";
                            db.SaveChanges();
                            System.Diagnostics.Debug.WriteLine($"✅ Cập nhật lịch đặt {hd.MaLich} thành 'Hoàn Thành'");
                        }
                    }

                    transaction.Commit();
                    System.Diagnostics.Debug.WriteLine($"✅ Thanh toán thành công: {hd.MaHD}");
                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("❌ ENTITY VALIDATION ERROR:");
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Entity: {validationErrors.Entry.Entity.GetType().Name}");
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine(
                                $"  Property: {validationError.PropertyName}\n" +
                                $"  Error: {validationError.ErrorMessage}");
                        }
                    }
                    return false;
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException updateEx)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine($"❌ DB UPDATE ERROR: {updateEx.Message}");
                    if (updateEx.InnerException != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ Inner Exception: {updateEx.InnerException.Message}");
                        if (updateEx.InnerException.InnerException != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Root Cause: {updateEx.InnerException.InnerException.Message}");
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine($"❌ GENERAL ERROR: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return false;
                }
            }
        }

        // ===== PHƯƠNG THỨC THANH TOÁN ĐƠN GIẢN (CHỈ HÓA ĐƠN, KHÔNG CHI TIẾT) =====
        public bool ThanhToanDonGian(HoaDon hd)
        {
            try
            {
                if (hd == null)
                {
                    System.Diagnostics.Debug.WriteLine("❌ HoaDon không được null");
                    return false;
                }

                if (string.IsNullOrEmpty(hd.MaHD))
                {
                    hd.MaHD = GenerateMaHoaDon();
                }

                if (!hd.ThoiGianThanhToan.HasValue)
                {
                    hd.ThoiGianThanhToan = DateTime.Now;
                }

                // ✅ ĐỔI: Để NULL thay vì "KVL"
                if (string.IsNullOrEmpty(hd.MaLich))
                {
                    hd.MaLich = null;
                }

                db.HoaDons.Add(hd);
                db.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"✅ Thanh toán đơn giản thành công: {hd.MaHD}");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi thanh toán đơn giản: {ex.Message}");
                return false;
            }
        }

        // ===== PHƯƠNG THỨC DISPOSE =====
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}