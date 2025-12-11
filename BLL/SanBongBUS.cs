using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class SanBongBUS
    {
        Model1 db = new Model1();

        public List<SanBong> GetListSanBong()
        {
            return db.SanBongs.ToList();
        }

        public List<string> GetListLoaiSan()
        {
            return db.SanBongs.Select(s => s.LoaiSan).Distinct().ToList();
        }

        public List<SanBong> GetSanByLoai(string loaiSan)
        {
            return db.SanBongs.Where(s => s.LoaiSan == loaiSan).ToList();
        }

        // MaSan là string (VD: "SB01")
        public decimal GetDonGia(string maSan, DateTime ngayDat)
        {
            string loaiNgay = "NgayThuong";
            if (ngayDat.DayOfWeek == DayOfWeek.Saturday || ngayDat.DayOfWeek == DayOfWeek.Sunday)
            {
                loaiNgay = "CuoiTuan";
            }

            // Tìm giá trong DB
            var gia = db.GiaSans.FirstOrDefault(g => g.MaSan == maSan && g.NgayTrongTuan == loaiNgay);

            // Nếu tìm thấy thì trả về giá, không thì trả về mặc định
            return gia != null ? (decimal)gia.DonGia : (loaiNgay == "NgayThuong" ? 149000 : 199000);
        }
    }
}