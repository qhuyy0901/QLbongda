using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class ThongKeBUS
    {
        private Model1 db = new Model1();

        // ===== LẤY DANH SÁCH NĂM CÓ DỮ LIỆU =====
        public List<int> GetListYears()
        {
            try
            {
                var years = db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue)
                    .Select(x => x.ThoiGianThanhToan.Value.Year)
                    .Distinct()
                    .OrderByDescending(y => y)
                    .ToList();

                if (years.Count == 0)
                {
                    // Nếu không có dữ liệu, trả về năm hiện tại
                    years.Add(DateTime.Now.Year);
                }

                return years;
            }
            catch
            {
                return new List<int> { DateTime.Now.Year };
            }
        }

        // ===== THỐNG KÊ THEO THÁNG (TRONG 1 NĂM) =====
        public Dictionary<int, decimal> GetRevenueByMonth(int year)
        {
            try
            {
                // Khởi tạo dict với 12 tháng, giá trị mặc định = 0
                Dictionary<int, decimal> result = new Dictionary<int, decimal>();
                for (int month = 1; month <= 12; month++)
                {
                    result[month] = 0;
                }

                // Lấy dữ liệu từ database
                var data = db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue &&
                                x.ThoiGianThanhToan.Value.Year == year &&
                                x.TongTien.HasValue)
                    .GroupBy(x => x.ThoiGianThanhToan.Value.Month)
                    .Select(g => new
                    {
                        Month = g.Key,
                        Total = g.Sum(x => x.TongTien.Value)
                    })
                    .ToList();

                // Gán giá trị từ database vào dict
                foreach (var item in data)
                {
                    if (result.ContainsKey(item.Month))
                    {
                        result[item.Month] = item.Total;
                    }
                }

                return result;
            }
            catch
            {
                // Trả về dict rỗng nếu lỗi
                Dictionary<int, decimal> emptyResult = new Dictionary<int, decimal>();
                for (int month = 1; month <= 12; month++)
                {
                    emptyResult[month] = 0;
                }
                return emptyResult;
            }
        }

        // ===== THỐNG KÊ THEO NĂM =====
        public Dictionary<int, decimal> GetRevenueByYear(int startYear = 2022, int endYear = 2025)
        {
            try
            {
                // Khởi tạo dict với các năm
                Dictionary<int, decimal> result = new Dictionary<int, decimal>();
                for (int year = startYear; year <= endYear; year++)
                {
                    result[year] = 0;
                }

                // Lấy dữ liệu từ database
                var data = db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue &&
                                x.TongTien.HasValue)
                    .GroupBy(x => x.ThoiGianThanhToan.Value.Year)
                    .Select(g => new
                    {
                        Year = g.Key,
                        Total = g.Sum(x => x.TongTien.Value)
                    })
                    .ToList();

                // Gán giá trị từ database vào dict
                foreach (var item in data)
                {
                    if (result.ContainsKey(item.Year))
                    {
                        result[item.Year] = item.Total;
                    }
                }

                return result;
            }
            catch
            {
                // Trả về dict rỗng nếu lỗi
                Dictionary<int, decimal> emptyResult = new Dictionary<int, decimal>();
                int currentYear = DateTime.Now.Year;
                for (int year = currentYear - 3; year <= currentYear; year++)
                {
                    emptyResult[year] = 0;
                }
                return emptyResult;
            }
        }

        // ===== LẤY TỔNG DOANH THU THEO NĂM =====
        public decimal GetTotalRevenueByYear(int year)
        {
            try
            {
                return db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue &&
                                x.ThoiGianThanhToan.Value.Year == year &&
                                x.TongTien.HasValue)
                    .Sum(x => x.TongTien.Value);
            }
            catch
            {
                return 0;
            }
        }

        // ===== LẤY TỔNG DOANH THU THEO THÁNG =====
        public decimal GetTotalRevenueByMonth(int year, int month)
        {
            try
            {
                return db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue &&
                                x.ThoiGianThanhToan.Value.Year == year &&
                                x.ThoiGianThanhToan.Value.Month == month &&
                                x.TongTien.HasValue)
                    .Sum(x => x.TongTien.Value);
            }
            catch
            {
                return 0;
            }
        }

        // ===== TÍNH DOANH THU LỊCH ĐẶT THEO THÁNG =====
        public Dictionary<int, decimal> GetRevenueByMonthFromLichDat(int year)
        {
            try
            {
                // Khởi tạo dict với 12 tháng, giá trị mặc định = 0
                Dictionary<int, decimal> result = new Dictionary<int, decimal>();
                for (int month = 1; month <= 12; month++)
                {
                    result[month] = 0;
                }

                // ===== LỊCH ĐẶT: Doanh thu từ LichDat (DonGiaThucTe) =====
                var lichData = db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue &&
                                x.ThoiGianThanhToan.Value.Year == year &&
                                !string.IsNullOrEmpty(x.MaLich))  // Có MaLich = từ lịch đặt
                    .GroupBy(x => x.ThoiGianThanhToan.Value.Month)
                    .Select(g => new
                    {
                        Month = g.Key,
                        Total = g.Sum(x => x.TongTien.HasValue ? x.TongTien.Value : 0)
                    })
                    .ToList();

                // Gán giá trị từ database vào dict
                foreach (var item in lichData)
                {
                    if (result.ContainsKey(item.Month))
                    {
                        result[item.Month] = item.Total;
                    }
                }

                return result;
            }
            catch
            {
                Dictionary<int, decimal> emptyResult = new Dictionary<int, decimal>();
                for (int month = 1; month <= 12; month++)
                {
                    emptyResult[month] = 0;
                }
                return emptyResult;
            }
        }

        // ===== TÍNH DOANH THU DỊCH VỤ THEO THÁNG =====
        public Dictionary<int, decimal> GetRevenueByMonthFromDichVu(int year)
        {
            try
            {
                // Khởi tạo dict với 12 tháng, giá trị mặc định = 0
                Dictionary<int, decimal> result = new Dictionary<int, decimal>();
                for (int month = 1; month <= 12; month++)
                {
                    result[month] = 0;
                }

                // ===== DỊCH VỤ: Doanh thu từ CT_HoaDon_DichVu =====
                var dichVuData = db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue &&
                                x.ThoiGianThanhToan.Value.Year == year)
                    .SelectMany(x => x.CT_HoaDon_DichVu)
                    .GroupBy(x => x.HoaDon.ThoiGianThanhToan.Value.Month)
                    .Select(g => new
                    {
                        Month = g.Key,
                        Total = g.Sum(x => x.ThanhTien.HasValue ? x.ThanhTien.Value : 0)
                    })
                    .ToList();

                // Gán giá trị từ database vào dict
                foreach (var item in dichVuData)
                {
                    if (result.ContainsKey(item.Month))
                    {
                        result[item.Month] = item.Total;
                    }
                }

                return result;
            }
            catch
            {
                Dictionary<int, decimal> emptyResult = new Dictionary<int, decimal>();
                for (int month = 1; month <= 12; month++)
                {
                    emptyResult[month] = 0;
                }
                return emptyResult;
            }
        }

        // ===== TÍNH DOANH THU LỊCH ĐẶT THEO NĂM =====
        public Dictionary<int, decimal> GetRevenueByYearFromLichDat(int startYear = 2022, int endYear = 2025)
        {
            try
            {
                Dictionary<int, decimal> result = new Dictionary<int, decimal>();
                for (int year = startYear; year <= endYear; year++)
                {
                    result[year] = 0;
                }

                var lichData = db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue &&
                                !string.IsNullOrEmpty(x.MaLich))  // Có MaLich
                    .GroupBy(x => x.ThoiGianThanhToan.Value.Year)
                    .Select(g => new
                    {
                        Year = g.Key,
                        Total = g.Sum(x => x.TongTien.HasValue ? x.TongTien.Value : 0)
                    })
                    .ToList();

                foreach (var item in lichData)
                {
                    if (result.ContainsKey(item.Year))
                    {
                        result[item.Year] = item.Total;
                    }
                }

                return result;
            }
            catch
            {
                Dictionary<int, decimal> emptyResult = new Dictionary<int, decimal>();
                int currentYear = DateTime.Now.Year;
                for (int year = currentYear - 3; year <= currentYear; year++)
                {
                    emptyResult[year] = 0;
                }
                return emptyResult;
            }
        }

        // ===== TÍNH DOANH THU DỊCH VỤ THEO NĂM =====
        public Dictionary<int, decimal> GetRevenueByYearFromDichVu(int startYear = 2022, int endYear = 2025)
        {
            try
            {
                Dictionary<int, decimal> result = new Dictionary<int, decimal>();
                for (int year = startYear; year <= endYear; year++)
                {
                    result[year] = 0;
                }

                var dichVuData = db.HoaDons
                    .Where(x => x.ThoiGianThanhToan.HasValue)
                    .SelectMany(x => x.CT_HoaDon_DichVu)
                    .GroupBy(x => x.HoaDon.ThoiGianThanhToan.Value.Year)
                    .Select(g => new
                    {
                        Year = g.Key,
                        Total = g.Sum(x => x.ThanhTien.HasValue ? x.ThanhTien.Value : 0)
                    })
                    .ToList();

                foreach (var item in dichVuData)
                {
                    if (result.ContainsKey(item.Year))
                    {
                        result[item.Year] = item.Total;
                    }
                }

                return result;
            }
            catch
            {
                Dictionary<int, decimal> emptyResult = new Dictionary<int, decimal>();
                int currentYear = DateTime.Now.Year;
                for (int year = currentYear - 3; year <= currentYear; year++)
                {
                    emptyResult[year] = 0;
                }
                return emptyResult;
            }
        }

        // ===== LẤY DOANH THU THEO SÂN (TỔNG) =====
        public List<dynamic> GetRevenueBySan()
        {
            try
            {
                var result = db.SanBongs
                    .Select(san => new
                    {
                        MaSan = san.MaSan,
                        TenSan = san.TenSan,
                        LoaiSan = san.LoaiSan,
                        DoanhThuLichDat = db.HoaDons
                            .Where(hd => hd.ThoiGianThanhToan.HasValue &&
                                        !string.IsNullOrEmpty(hd.MaLich) &&
                                        hd.LichDat.MaSan == san.MaSan &&
                                        hd.TongTien.HasValue)
                            .Sum(hd => hd.TongTien.Value),
                        DoanhThuDichVu = db.HoaDons
                            .Where(hd => hd.ThoiGianThanhToan.HasValue &&
                                        hd.TongTien.HasValue)
                            .SelectMany(hd => hd.CT_HoaDon_DichVu)
                            .Where(ct => ct.HoaDon.LichDat.MaSan == san.MaSan)
                            .Sum(ct => ct.ThanhTien.HasValue ? ct.ThanhTien.Value : 0),
                        SoLanDat = db.LichDats
                            .Where(ld => ld.MaSan == san.MaSan && ld.TrangThai == "Đã đặt")
                            .Count()
                    })
                    .OrderByDescending(x => (decimal)x.DoanhThuLichDat + (decimal)x.DoanhThuDichVu)
                    .Cast<dynamic>()
                    .ToList();

                return result;
            }
            catch
            {
                return new List<dynamic>();
            }
        }

        // ===== LẤY DOANH THU THEO SÂN THEO THÁNG =====
        public Dictionary<string, Dictionary<int, decimal>> GetRevenueBySanByMonth(int year)
        {
            try
            {
                var result = new Dictionary<string, Dictionary<int, decimal>>();

                var sans = db.SanBongs.ToList();

                foreach (var san in sans)
                {
                    var doanhThuTheoThang = new Dictionary<int, decimal>();

                    for (int month = 1; month <= 12; month++)
                    {
                        decimal doanhThuLichDat = db.HoaDons
                            .Where(hd => hd.ThoiGianThanhToan.HasValue &&
                                        hd.ThoiGianThanhToan.Value.Year == year &&
                                        hd.ThoiGianThanhToan.Value.Month == month &&
                                        !string.IsNullOrEmpty(hd.MaLich) &&
                                        hd.LichDat.MaSan == san.MaSan &&
                                        hd.TongTien.HasValue)
                            .Sum(hd => hd.TongTien.Value);

                        decimal doanhThuDichVu = db.HoaDons
                            .Where(hd => hd.ThoiGianThanhToan.HasValue &&
                                        hd.ThoiGianThanhToan.Value.Year == year &&
                                        hd.ThoiGianThanhToan.Value.Month == month)
                            .SelectMany(hd => hd.CT_HoaDon_DichVu)
                            .Where(ct => ct.HoaDon.LichDat.MaSan == san.MaSan)
                            .Sum(ct => ct.ThanhTien.HasValue ? ct.ThanhTien.Value : 0);

                        doanhThuTheoThang[month] = doanhThuLichDat + doanhThuDichVu;
                    }

                    result[san.MaSan] = doanhThuTheoThang;
                }

                return result;
            }
            catch
            {
                return new Dictionary<string, Dictionary<int, decimal>>();
            }
        }
    }
}