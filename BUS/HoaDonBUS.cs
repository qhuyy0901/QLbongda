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

        // ===== LẤY TẤT CẢ HÓA ĐƠN =====
        public List<HoaDon> GetAll()
        {
            try
            {
                return db.HoaDons
                    .OrderByDescending(x => x.ThoiGianThanhToan)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetAll: {ex.Message}");
                return new List<HoaDon>();
            }
        }

        // ===== LẤY HÓA ĐƠN THEO MÃ =====
        public HoaDon GetById(string maHD)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maHD))
                    return null;

                return db.HoaDons.FirstOrDefault(x => x.MaHD == maHD);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetById: {ex.Message}");
                return null;
            }
        }

        // ===== LẤY HÓA ĐƠN THEO KHOẢNG NGÀY =====
        public List<HoaDon> GetByDateRange(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                return db.HoaDons
                    .Where(x => x.ThoiGianThanhToan >= tuNgay && x.ThoiGianThanhToan <= denNgay)
                    .OrderByDescending(x => x.ThoiGianThanhToan)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetByDateRange: {ex.Message}");
                return new List<HoaDon>();
            }
        }

        // ===== TÌM KIẾM HÓA ĐƠN =====
        public List<dynamic> Search(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return GetAll().Cast<dynamic>().ToList();

                string searchTerm = keyword.Trim().ToLower();

                var result = db.HoaDons
                    .Where(x =>
                        x.MaHD.ToLower().Contains(searchTerm) ||
                        (x.MaLich != null && x.MaLich.ToLower().Contains(searchTerm)) ||
                        (x.HinhThucTT != null && x.HinhThucTT.ToLower().Contains(searchTerm))
                    )
                    .OrderByDescending(x => x.ThoiGianThanhToan)
                    .Cast<dynamic>()
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi Search: {ex.Message}");
                return new List<dynamic>();
            }
        }

        // ===== HÀM THANH TOÁN (TRANSACTION) =====
        public bool ThanhToan(HoaDon hd, List<CT_HoaDon_DichVu> listChiTiet)
        {
            using (var transaction = db.Database.BeginTransaction())
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

                    if (string.IsNullOrEmpty(hd.MaLich) || hd.MaLich == "KVL")
                    {
                        hd.MaLich = null;
                    }
                    else
                    {
                        var lichDat = db.LichDats.FirstOrDefault(x => x.MaLich == hd.MaLich);
                        if (lichDat == null)
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Lịch đặt {hd.MaLich} không tồn tại");
                            return false;
                        }
                    }

                    db.HoaDons.Add(hd);
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine($"✅ Lưu hóa đơn thành công: {hd.MaHD}");

                    if (listChiTiet != null && listChiTiet.Count > 0)
                    {
                        int stt = 1;
                        foreach (var item in listChiTiet)
                        {
                            if (string.IsNullOrEmpty(item.MaCT))
                            {
                                item.MaCT = $"CT{hd.MaHD.Substring(2)}{stt:D2}";
                                stt++;
                            }

                            item.MaHD = hd.MaHD;

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
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine($"❌ Lỗi ThanhToan: {ex.Message}");
                    return false;
                }
            }
        }

        // ===== PHƯƠNG THỨC THANH TOÁN ĐƠN GIẢN =====
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
                    hd.MaLich = null;
                }

                db.HoaDons.Add(hd);
                db.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"✅ Thanh toán đơn giản thành công: {hd.MaHD}");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi ThanhToanDonGian: {ex.Message}");
                return false;
            }
        }

        // ===== LẤY DANH SÁCH HÓA ĐƠN VỚI THÔNG TIN KHÁCH HÀNG =====
        public List<dynamic> GetHoaDonWithCustomerInfo()
        {
            try
            {
                var result = (from hd in db.HoaDons
                              join ld in db.LichDats on hd.MaLich equals ld.MaLich into lichGroup
                              from ld in lichGroup.DefaultIfEmpty()
                              select new
                              {
                                  MaHD = hd.MaHD,
                                  MaLich = hd.MaLich,
                                  TenKH = ld != null ? ld.TenKH : "Khách vãng lai",
                                  SDT_KH = ld != null ? ld.SDT_KH : "N/A",
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
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetHoaDonWithCustomerInfo: {ex.Message}");
                return new List<dynamic>();
            }
        }

        // ===== LẤY HÓA ĐƠN THEO SỐ ĐIỆN THOẠI =====
        public List<dynamic> GetHoaDonBySdt(string sdt)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sdt))
                    return new List<dynamic>();

                var result = (from hd in db.HoaDons
                              join ld in db.LichDats on hd.MaLich equals ld.MaLich into lichGroup
                              from ld in lichGroup.DefaultIfEmpty()
                              where ld != null && ld.SDT_KH == sdt
                              select new
                              {
                                  MaHD = hd.MaHD,
                                  MaLich = hd.MaLich,
                                  TenKH = ld.TenKH,
                                  SDT_KH = ld.SDT_KH,
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
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetHoaDonBySdt: {ex.Message}");
                return new List<dynamic>();
            }
        }

        // ===== LẤY CHI TIẾT HÓA ĐƠN =====
        public List<CT_HoaDon_DichVu> GetChiTietHoaDon(string maHD)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maHD))
                    return new List<CT_HoaDon_DichVu>();

                var result = db.CT_HoaDon_DichVu
                    .Where(x => x.MaHD == maHD)
                    .ToList();

                // Load thông tin dịch vụ
                foreach (var ct in result)
                {
                    if (!string.IsNullOrEmpty(ct.MaDV))
                    {
                        ct.DichVu = db.DichVus.FirstOrDefault(x => x.MaDV == ct.MaDV);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi GetChiTietHoaDon: {ex.Message}");
                return new List<CT_HoaDon_DichVu>();
            }
        }

        // ===== CẬP NHẬT HÓA ĐƠN =====
        public bool Update(HoaDon hd)
        {
            try
            {
                if (hd == null || string.IsNullOrWhiteSpace(hd.MaHD))
                    return false;

                var item = db.HoaDons.Find(hd.MaHD);
                if (item == null)
                    return false;

                item.TongTien = hd.TongTien;
                item.HinhThucTT = hd.HinhThucTT;
                item.ThoiGianThanhToan = hd.ThoiGianThanhToan;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi Update: {ex.Message}");
                return false;
            }
        }

        // ===== XÓA HÓA ĐƠN =====
        public bool Delete(string maHD)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maHD))
                    return false;

                var item = db.HoaDons.Find(maHD);
                if (item == null)
                    return false;

                // Xóa chi tiết hóa đơn trước
                var chiTiets = db.CT_HoaDon_DichVu.Where(x => x.MaHD == maHD).ToList();
                foreach (var ct in chiTiets)
                {
                    db.CT_HoaDon_DichVu.Remove(ct);
                }

                db.HoaDons.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi Delete: {ex.Message}");
                return false;
            }
        }

        // ===== DISPOSE =====
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}