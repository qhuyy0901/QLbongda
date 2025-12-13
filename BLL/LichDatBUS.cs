using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Diagnostics;
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
        // nhớ thêm dòng này

        public bool Insert(LichDat lich)
        {
            try
            {
                // 1. Validate cơ bản
                if (!lich.NgayDat.HasValue)
                    throw new Exception("Chưa chọn ngày đặt");

                if (lich.GioBD >= lich.GioKT)
                    throw new Exception("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");

                DateTime ngay = lich.NgayDat.Value.Date;

                // 2. CHECK / TẠO KHÁCH HÀNG
                if (!string.IsNullOrWhiteSpace(lich.SDT_KH))
                {
                    var kh = db.KhachHangs
                               .FirstOrDefault(x => x.SDT_KH == lich.SDT_KH);

                    if (kh == null)
                    {
                        Debug.WriteLine($"➕ Tạo mới khách hàng: {lich.SDT_KH}");

                        kh = new KhachHang
                        {
                            SDT_KH = lich.SDT_KH,
                            TenKH = lich.TenKH
                        };

                        db.KhachHangs.Add(kh);
                        db.SaveChanges(); // ⚠️ PHẢI SAVE TRƯỚC
                    }
                }

                // 3. Check trùng giờ (cùng sân – cùng ngày)
                bool checkTrung = db.LichDats.Any(x =>
                    x.MaSan == lich.MaSan &&
                    x.NgayDat.HasValue &&
                    x.NgayDat.Value == ngay &&
                    lich.GioBD < x.GioKT &&
                    lich.GioKT > x.GioBD
                );

                if (checkTrung)
                    throw new Exception("Khung giờ đã bị trùng");

                // 4. Insert lịch đặt
                db.LichDats.Add(lich);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert LichDat lỗi: " + ex.Message);
            }
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