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

                    // ✅ KIỂM TRA: MaLich bắt buộc phải tồn tại
                    if (string.IsNullOrEmpty(hd.MaLich))
                    {
                        System.Diagnostics.Debug.WriteLine("❌ MaLich không được rỗng - yêu cầu phải có lịch đặt");
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

                    // ✅ KIỂM TRA: MaLich phải tồn tại trong bảng LichDat
                    var lichDat = db.LichDats.FirstOrDefault(x => x.MaLich == hd.MaLich);
                    if (lichDat == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ Lịch đặt {hd.MaLich} không tồn tại trong database");
                        return false;
                    }

                    // ✅ KIỂM TRA: Lịch đặt phải ở trạng thái "Đã đặt"
                    if (lichDat.TrangThai != "Đã đặt")
                    {
                        System.Diagnostics.Debug.WriteLine($"⚠️ Lịch đặt {hd.MaLich} không ở trạng thái 'Đã đặt'. Trạng thái hiện tại: {lichDat.TrangThai}");
                        return false;
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

                    // 3️⃣ CẬP NHẬT TRẠNG THÁI LỊCH ĐẶT: "Đã đặt" → "Đã thanh toán"
                    if (!string.IsNullOrEmpty(hd.MaLich))
                    {
                        // ✅ Re-fetch để đảm bảo không bị lỗi state
                        var lichDatUpdate = db.LichDats.FirstOrDefault(x => x.MaLich == hd.MaLich);
                        if (lichDatUpdate != null)
                        {
                            lichDatUpdate.TrangThai = "Đã thanh toán";
                            db.SaveChanges();
                            System.Diagnostics.Debug.WriteLine($"✅ Cập nhật trạng thái lịch đặt {hd.MaLich}: 'Đã đặt' → 'Đã thanh toán'");
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

        // ===== PHƯƠNG THỨC DISPOSE =====
        public void Dispose()
        {
            db?.Dispose();
        }

        public decimal GetTongDoanhThuDichVu(int nam, int thang)
        {
            try
            {
                var query = from ct in db.CT_HoaDon_DichVu
                            join hd in db.HoaDons on ct.MaHD equals hd.MaHD
                            where hd.ThoiGianThanhToan.HasValue
                            select new { ct.ThanhTien, hd.ThoiGianThanhToan };

                // ✅ SỬA LẠI ĐOẠN NÀY: Chỉ lọc năm nếu nam > 0
                if (nam > 0)
                {
                    query = query.Where(x => x.ThoiGianThanhToan.Value.Year == nam);
                }

                // Lọc tháng nếu có chọn tháng
                if (thang > 0)
                {
                    query = query.Where(x => x.ThoiGianThanhToan.Value.Month == thang);
                }

                decimal? total = query.Sum(x => x.ThanhTien);
                return total ?? 0;
            }
            catch
            {
                return 0;
            }
        }



        // ===== LẤY DANH SÁCH HÓA ĐƠN VỚI THÔNG TIN KHÁCH HÀNG VÀ LỊCH ĐẶT =====
        public List<dynamic> GetHoaDonWithCustomerInfo()
        {
            try
            {
                var result = (from hd in db.HoaDons
                              join ld in db.LichDats on hd.MaLich equals ld.MaLich
                              select new
                              {
                                  MaHD = hd.MaHD,
                                  MaLich = hd.MaLich,
                                  TenKH = ld.TenKH,
                                  SDT_KH = ld.SDT_KH,
                                  NgayDat = ld.NgayDat,
                                  MaSan = ld.MaSan,
                                  GioBD = ld.GioBD,
                                  GioKT = ld.GioKT,
                                  TrangThaiLich = ld.TrangThai,
                                  TongTien = hd.TongTien,
                                  ThoiGianThanhToan = hd.ThoiGianThanhToan,
                                  HinhThucTT = hd.HinhThucTT
                              })
                       .AsEnumerable()
                       .Select(x => (dynamic)x)
                       .ToList();

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi lấy danh sách hóa đơn: {ex.Message}");
                return new List<dynamic>();
            }
        }

        // ===== LẤY DANH SÁCH HÓA ĐƠN THEO SỐ ĐIỆN THOẠI =====
        public List<dynamic> GetHoaDonBySdt(string sdt)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sdt))
                    return new List<dynamic>();

                var result = (from hd in db.HoaDons
                              join ld in db.LichDats on hd.MaLich equals ld.MaLich
                              where ld.SDT_KH == sdt
                              select new
                              {
                                  MaHD = hd.MaHD,
                                  MaLich = hd.MaLich,
                                  TenKH = ld.TenKH,
                                  SDT_KH = ld.SDT_KH,
                                  NgayDat = ld.NgayDat,
                                  MaSan = ld.MaSan,
                                  GioBD = ld.GioBD,
                                  GioKT = ld.GioKT,
                                  TrangThaiLich = ld.TrangThai,
                                  TongTien = hd.TongTien,
                                  ThoiGianThanhToan = hd.ThoiGianThanhToan,
                                  HinhThucTT = hd.HinhThucTT
                              })
                              .AsEnumerable()
                              .Select(x => (dynamic)x)
                              .ToList();

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi lấy hóa đơn theo SĐT: {ex.Message}");
                return new List<dynamic>();
            }
        }

        // ===== LẤY CHI TIẾT HÓA ĐƠN THEO MÃ HÓA ĐƠN =====
        public List<CT_HoaDon_DichVu> GetChiTietHoaDon(string maHD)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maHD))
                    return new List<CT_HoaDon_DichVu>();

                var result = db.CT_HoaDon_DichVu
                    .Where(x => x.MaHD == maHD)
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi lấy chi tiết hóa đơn: {ex.Message}");
                return new List<CT_HoaDon_DichVu>();
            }
        }

        // ===== LẤY TRẠNG THÁI LỊCH ĐẶT =====
        public string GetTrangThaiLichDat(string maLich)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maLich))
                    return null;

                var lich = db.LichDats.FirstOrDefault(x => x.MaLich == maLich);
                return lich?.TrangThai;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi lấy trạng thái lịch: {ex.Message}");
                return null;
            }
        }
    }
}