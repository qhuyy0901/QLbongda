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

                    // ===== NẾU KHÁCH VÃNG LAI, ĐẶT MALICH = "KVL" (MÃ KHÁCH VÃNG LAI) =====
                    // Không được để null vì MaLich là [Required]
                    if (string.IsNullOrEmpty(hd.MaLich))
                    {
                        hd.MaLich = "KVL";
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
                                // Lấy đơn giá từ bảng DichVu nếu có
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

                    // 3️⃣ CẬP NHẬT TRẠNG THÁI LỊCH ĐẶT (NẾU CÓ - CHỈ KHÔNG PHẢI KHÁCH VÃNG LAI)
                    if (!string.IsNullOrEmpty(hd.MaLich) && hd.MaLich != "KVL")
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
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine(
                                $"Entity: {validationErrors.Entry.Entity.GetType().Name}\n" +
                                $"Property: {validationError.PropertyName}\n" +
                                $"Error: {validationError.ErrorMessage}\n");
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
                        System.Diagnostics.Debug.WriteLine($"Inner: {updateEx.InnerException.Message}");
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine($"❌ GENERAL ERROR: {ex.Message}\n{ex.StackTrace}");
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

                if (string.IsNullOrEmpty(hd.MaLich))
                {
                    hd.MaLich = "KVL";
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