using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class LichDatBUS
    {
        // Khởi tạo Model (Entity Framework)
        private Model1 db = new Model1();

        // 1. Lấy tất cả lịch đặt (Sắp xếp mới nhất lên đầu)
        public List<LichDat> GetAll()
        {
            return db.LichDats.OrderByDescending(x => x.NgayDat).ThenByDescending(x => x.GioBD).ToList();
        }

        // 2. Lấy lịch theo ngày (Để tô màu sân)
        public List<LichDat> GetByDate(DateTime date)
        {
            // So sánh phần ngày (Date)
            return db.LichDats.Where(x => x.NgayDat == date.Date).ToList();
        }

        // 3. Thêm mới (Có kiểm tra trùng giờ)
        public bool Insert(LichDat lich)
        {
            try
            {
                // Logic kiểm tra trùng giờ:
                // (Giờ BĐ mới < Giờ KT cũ) VÀ (Giờ KT mới > Giờ BĐ cũ) trong cùng 1 ngày và cùng 1 sân
                var checkTrung = db.LichDats.FirstOrDefault(x =>
                    x.MaSan == lich.MaSan &&
                    x.NgayDat == lich.NgayDat &&
                    x.GioBD < lich.GioKT && x.GioKT > lich.GioBD);

                if (checkTrung != null) return false; // Đã bị trùng

                db.LichDats.Add(lich);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        // 4. Sửa thông tin
        public bool Update(LichDat lich)
        {
            try
            {
                var item = db.LichDats.Find(lich.MaLich);
                if (item == null) return false;

                // Cập nhật dữ liệu
                item.MaSan = lich.MaSan;
                item.SDT_KH = lich.SDT_KH;
                item.TenKH = lich.TenKH;
                item.NgayDat = lich.NgayDat;
                item.GioBD = lich.GioBD;
                item.GioKT = lich.GioKT;
                item.DonGiaThucTe = lich.DonGiaThucTe;
                item.TrangThai = lich.TrangThai;

                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        // 5. Xóa
        public bool Delete(int maLich)
        {
            try
            {
                var item = db.LichDats.Find(maLich);
                if (item == null) return false;

                db.LichDats.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}