using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class LichDatBUS
    {
        Model1 db = new Model1();

        public List<LichDat> GetListLichDat()
        {
            return db.LichDats.OrderByDescending(x => x.NgayDat).ToList();
        }

        public List<LichDat> GetLichDatByDate(DateTime date)
        {
            return db.LichDats.Where(x => x.NgayDat == date && x.TrangThai != "Đã Hủy").ToList();
        }

        public string ThemLichDat(LichDat lichMoi, string tenKH)
        {
            try
            {
                if (lichMoi.GioKT <= lichMoi.GioBD) return "Giờ kết thúc phải lớn hơn giờ bắt đầu!";

                // Kiểm tra trùng lịch (MaSan là string)
                var biTrung = db.LichDats.Any(x =>
                    x.MaSan == lichMoi.MaSan &&
                    x.NgayDat == lichMoi.NgayDat &&
                    x.TrangThai != "Đã Hủy" &&
                    (x.GioBD < lichMoi.GioKT && x.GioKT > lichMoi.GioBD)
                );

                if (biTrung) return "Sân đã bị đặt trong khung giờ này!";

                // Tự động thêm khách hàng
                var khach = db.KhachHangs.Find(lichMoi.SDT_KH);
                if (khach == null)
                {
                    db.KhachHangs.Add(new KhachHang { SDT_KH = lichMoi.SDT_KH, TenKH = tenKH });
                }
                else { khach.TenKH = tenKH; }
                db.SaveChanges();

                // Lưu lịch
                db.LichDats.Add(lichMoi);
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex) { return "Lỗi: " + ex.Message; }
        }

        public void HuyLichDat(int maLich)
        {
            var lich = db.LichDats.Find(maLich);
            if (lich != null) { lich.TrangThai = "Đã Hủy"; db.SaveChanges(); }
        }

        public string XoaLichDat(int maLich)
        {
            try
            {
                var lich = db.LichDats.Find(maLich);
                if (lich != null) { db.LichDats.Remove(lich); db.SaveChanges(); return "Success"; }
                return "Không tìm thấy lịch";
            }
            catch { return "Không thể xóa (dính khóa ngoại)"; }
        }
    }
}