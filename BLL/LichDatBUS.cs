using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
namespace BUS
{
    public class LichDatBUS
    {
        private Model1 db = new Model1();

        public List<LichDat> GetAll()
        {
            return db.LichDats.OrderByDescending(x => x.NgayDat).ThenByDescending(x => x.GioBD).ToList();
        }

        public List<LichDat> Search(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return GetAll();

                string searchTerm = keyword.Trim().ToLower();

                var result = db.LichDats.Where(x =>
                    x.MaLich.ToLower().Contains(searchTerm) ||
                    x.MaSan.ToLower().Contains(searchTerm) ||
                    x.SDT_KH.ToLower().Contains(searchTerm) ||
                    x.TenKH.ToLower().Contains(searchTerm) ||
                    x.TrangThai.ToLower().Contains(searchTerm)
                ).OrderByDescending(x => x.NgayDat)
                 .ThenByDescending(x => x.GioBD)
                 .ToList();

                return result;
            }
            catch
            {
                return GetAll();
            }
        }

        public bool Insert(LichDat lich)
        {
            try
            {
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
                        db.SaveChanges();
                    }
                }

                bool checkTrung = db.LichDats.Any(x =>
                    x.MaSan == lich.MaSan &&
                    x.NgayDat.HasValue &&
                    x.NgayDat.Value == ngay &&
                    lich.GioBD < x.GioKT &&
                    lich.GioKT > x.GioBD
                );

                if (checkTrung)
                {
                    // SỬA LẠI DÒNG NÀY
                    throw new Exception($"Sân {lich.MaSan} vào khung giờ {lich.GioBD}h - {lich.GioKT}h đã được đặt rồi!");
                }

                db.LichDats.Add(lich);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert LichDat lỗi: " + ex.Message);
            }
        }

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

        public bool Delete(string maLich)
        {
            try
            {
                // Tìm lịch đặt theo Mã Lịch
                var item = db.LichDats.Find(maLich);

                if (item == null)
                    throw new Exception("Không tìm thấy mã lịch này");

                // Xóa bản ghi
                db.LichDats.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa lịch: " + ex.Message);
            }
        }

        public bool HuyDat(string maLich)
        {
            try
            {
                // Tìm lịch đặt theo Mã Lịch
                var item = db.LichDats.Find(maLich);
            
                if (item == null) 
                    throw new Exception("Không tìm thấy mã lịch này");

                // Cập nhật trạng thái
                item.TrangThai = "Đã hủy";

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi hủy sân: " + ex.Message);
            }
        }
    }
}