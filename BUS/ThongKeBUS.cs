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
    }
}