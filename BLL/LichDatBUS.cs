using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace BUS
{
    public class LichDatBUS
    {
        private Model1 db = new Model1();

        // ===== TỰ ĐỘNG SINH MÃ LỊCH =====
        private string GenerateMaLich()
        {
            try
            {
                int count = db.LichDats.Count();
                string maLich = $"LD{(count + 1):D3}";
                
                while (db.LichDats.Any(x => x.MaLich == maLich))
                {
                    count++;
                    maLich = $"LD{count:D3}";
                }
                
                return maLich;
            }
            catch
            {
                return "LD" + DateTime.Now.Ticks.ToString().Substring(0, 10);
            }
        }

        // ===== VALIDATE TÊN KHÁCH HÀNG =====
        private bool IsValidCustomerName(string tenKH)
        {
            if (string.IsNullOrWhiteSpace(tenKH))
                throw new Exception("❌ Tên khách hàng không được để trống");

            string cleanName = tenKH.Trim();

            if (!Regex.IsMatch(cleanName, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                throw new Exception("❌ Tên khách hàng chỉ được phép chứa chữ cái và khoảng trắng!\n\n💡 Không được dùng số hoặc ký tự đặc biệt.");
            }

            if (cleanName.Length < 2 || cleanName.Length > 50)
            {
                throw new Exception("❌ Tên khách hàng phải từ 2 đến 50 ký tự");
            }

            return true;
        }

        // ===== VALIDATE SỐ ĐIỆN THOẠI =====
        private bool IsValidPhoneNumberFormat(string sdt)
        {
            if (string.IsNullOrWhiteSpace(sdt))
                throw new Exception("❌ Số điện thoại không được để trống");

            string cleanPhone = sdt.Trim();

            if (!Regex.IsMatch(cleanPhone, @"^\d+$"))
            {
                throw new Exception("❌ Số điện thoại chỉ được chứa chữ số!");
            }

            if (cleanPhone.Length != 10)
            {
                throw new Exception($"❌ Số điện thoại phải có đúng 10 chữ số!\n\nSố bạn nhập: {cleanPhone.Length} chữ số");
            }

            if (!cleanPhone.StartsWith("0"))
            {
                throw new Exception("❌ Số điện thoại phải bắt đầu bằng số 0!\n\n💡 Ví dụ: 0912345678");
            }

            return true;
        }

        // ===== VALIDATE MÃ SÂN (CHỈ CHẤP NHẬN SAN1-SAN6) =====
        private bool IsValidSanCode(string maSan)
        {
            if (string.IsNullOrWhiteSpace(maSan))
                throw new Exception("❌ Mã sân không được để trống");

            string cleanMaSan = maSan.Trim();

            // ===== DANH SÁCH SÂN HỢP LỆ (CÓ THỂ VIẾT THƯỜNG HOẶC HOA) =====
            string[] validSans = { "San1", "San2", "San3", "San4", "San5", "San6" };

            // ===== KIỂM TRA CÓ TRONG DANH SÁCH KHÔNG (SO SÁNH CASE-INSENSITIVE) =====
            bool isValidSan = validSans.Any(s => s.Equals(cleanMaSan, StringComparison.OrdinalIgnoreCase));
            
            if (!isValidSan)
            {
                throw new Exception(
                    $"❌ Mã sân '{maSan}' không hợp lệ!\n\n" +
                    $"💡 Sân hợp lệ: {string.Join(", ", validSans)}"
                );
            }

            // ===== KIỂM TRA TỒN TẠI TRONG BẢNG SanBong VÀ HOẠT ĐỘNG =====
            var sanBong = db.SanBongs.FirstOrDefault(x => 
                x.MaSan != null && 
                x.MaSan.Trim().Equals(cleanMaSan, StringComparison.OrdinalIgnoreCase) &&
                x.TrangThai != "Không hoạt động"
            );
            
            if (sanBong == null)
            {
                throw new Exception(
                    $"❌ Sân '{cleanMaSan}' không tồn tại hoặc không hoạt động!\n\n" +
                    $"💡 Sân hợp lệ: {string.Join(", ", validSans)}"
                );
            }

            return true;
        }

        // ===== VALIDATE ĐƠN GIÁ =====
        private bool IsValidPrice(decimal? price)
        {
            if (!price.HasValue)
                throw new Exception("❌ Đơn giá không được để trống");

            decimal priceValue = price.Value;

            if (priceValue < 0)
                throw new Exception("❌ Đơn giá không được âm!");

            if (priceValue == 0)
                throw new Exception("❌ Đơn giá phải lớn hơn 0!");

            if (priceValue > 999999999)
                throw new Exception("❌ Đơn giá quá lớn (tối đa 999,999,999)!");

            return true;
        }

        // ===== KIỂM TRA TRÙNG LẠP SÂN VÀ KHUNG GIỜ (CẬP NHẬT) =====
        private bool IsTimeSlotConflict(string maSan, DateTime ngayDat, int gioBD, int gioKT, string excludeMaLich = null)
        {
            try
            {
                DateTime ngay = ngayDat.Date;

                // ===== CHỈ KIỂM TRA TRÙNG: CÙNG SÂN + CÙNG NGÀY + CÙNG KHUNG GIỜ =====
                // KHÔNG kiểm tra SĐT - cho phép cùng 1 SĐT đặt nhiều sân khác nhau
                var query = db.LichDats.Where(x =>
                    x.MaSan == maSan &&                                    // ✅ CÙNG SÂN
                    x.NgayDat.HasValue &&
                    x.NgayDat.Value == ngay &&                             // ✅ CÙNG NGÀY
                    gioBD < x.GioKT &&
                    gioKT > x.GioBD &&                                     // ✅ TRÙNG KHUNG GIỜ
                    x.TrangThai != "Đã hủy" &&
                    x.TrangThai != "Đã xóa"
                );

                if (!string.IsNullOrWhiteSpace(excludeMaLich))
                {
                    query = query.Where(x => x.MaLich != excludeMaLich);
                }

                return query.Any();
            }
            catch
            {
                return false;
            }
        }

        public List<LichDat> GetAll()
        {
            return db.LichDats
                .Where(x => x.TrangThai != "Đã xóa")
                .OrderByDescending(x => x.NgayDat)
                .ThenByDescending(x => x.GioBD)
                .ToList();
        }

        public List<LichDat> Search(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return GetAll();

                string searchTerm = keyword.Trim().ToLower();

                var result = db.LichDats
                    .Where(x => x.TrangThai != "Đã xóa")
                    .Where(x =>
                        x.MaLich.ToLower().Contains(searchTerm) ||
                        x.MaSan.ToLower().Contains(searchTerm) ||
                        x.SDT_KH.ToLower().Contains(searchTerm) ||
                        x.TenKH.ToLower().Contains(searchTerm) ||
                        x.TrangThai.ToLower().Contains(searchTerm)
                    )
                    .OrderByDescending(x => x.NgayDat)
                    .ThenByDescending(x => x.GioBD)
                    .ToList();

                return result;
            }
            catch
            {
                return GetAll();
            }
        }

        // ===== XÓA HOẶC BỎ DÙNG METHOD CŨ =====
        // Giữ lại nhưng đổi tên để tránh nhầm lẫn
        [Obsolete("Không còn sử dụng - sử dụng IsTimeSlotConflict thay thế")]
        public bool IsPhoneNumberAlreadyBooked(string sdt, DateTime ngayDat)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sdt))
                    return false;

                DateTime ngay = ngayDat.Date;

                bool exists = db.LichDats.Any(x =>
                    x.SDT_KH == sdt &&
                    x.NgayDat.HasValue &&
                    x.NgayDat.Value == ngay &&
                    x.TrangThai != "Đã hủy" &&
                    x.TrangThai != "Đã xóa"
                );

                return exists;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert(LichDat lich)
        {
            try
            {
                // ===== VALIDATE THỜI GIAN =====
                if (!lich.NgayDat.HasValue)
                    throw new Exception("❌ Chưa chọn ngày đặt");

                if (!lich.GioBD.HasValue || !lich.GioKT.HasValue)
                    throw new Exception("❌ Chưa chọn giờ đặt");

                if (lich.GioBD >= lich.GioKT)
                    throw new Exception("❌ Giờ bắt đầu phải nhỏ hơn giờ kết thúc");

                DateTime ngay = lich.NgayDat.Value.Date;

                // ===== VALIDATE TÊN KHÁCH HÀNG =====
                IsValidCustomerName(lich.TenKH);

                // ===== VALIDATE SỐ ĐIỆN THOẠI =====
                IsValidPhoneNumberFormat(lich.SDT_KH);

                // ===== VALIDATE MÃ SÂN =====
                IsValidSanCode(lich.MaSan);

                // ===== VALIDATE ĐƠN GIÁ =====
                IsValidPrice(lich.DonGiaThucTe);

                // ===== KIỂM TRA TRÙNG SÂN VÀ KHUNG GIỜ =====
                if (IsTimeSlotConflict(lich.MaSan, lich.NgayDat.Value, lich.GioBD.Value, lich.GioKT.Value))
                {
                    throw new Exception($"❌ Sân {lich.MaSan} vào khung giờ {lich.GioBD}h - {lich.GioKT}h ngày {ngay:dd/MM/yyyy} đã được đặt rồi!\n\n💡 Vui lòng chọn khung giờ khác hoặc sân khác.");
                }

                // ===== CHECK / TẠO KHÁCH HÀNG =====
                if (!string.IsNullOrWhiteSpace(lich.SDT_KH))
                {
                    var kh = db.KhachHangs.FirstOrDefault(x => x.SDT_KH == lich.SDT_KH);

                    if (kh == null)
                    {
                        kh = new KhachHang
                        {
                            SDT_KH = lich.SDT_KH,
                            TenKH = lich.TenKH
                        };

                        db.KhachHangs.Add(kh);
                        db.SaveChanges();
                    }
                }

                if (string.IsNullOrWhiteSpace(lich.TrangThai))
                    lich.TrangThai = "Đã đặt";

                // ===== SINH MÃ LỊCH TỰ ĐỘNG =====
                lich.MaLich = GenerateMaLich();

                db.LichDats.Add(lich);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("❌ Insert LichDat lỗi: " + ex.Message);
            }
        }

        public bool Update(LichDat lich)
        {
            try
            {
                var item = db.LichDats.Find(lich.MaLich);
                if (item == null) 
                    throw new Exception("❌ Không tìm thấy mã lịch này");

                // ===== VALIDATE THỜI GIAN =====
                if (lich.GioBD >= lich.GioKT)
                    throw new Exception("❌ Giờ bắt đầu phải nhỏ hơn giờ kết thúc");

                // ===== VALIDATE TÊN KHÁCH HÀNG =====
                IsValidCustomerName(lich.TenKH);

                // ===== VALIDATE SỐ ĐIỆN THOẠI =====
                IsValidPhoneNumberFormat(lich.SDT_KH);

                // ===== VALIDATE MÃ SÂN =====
                IsValidSanCode(lich.MaSan);

                // ===== VALIDATE ĐƠN GIÁ =====
                IsValidPrice(lich.DonGiaThucTe);

                // ===== KIỂM TRA TRÙNG LẠP NẾU THAY ĐỔI SÂN HOẶC THỜI GIAN =====
                if (lich.MaSan != item.MaSan || 
                    lich.NgayDat != item.NgayDat || 
                    lich.GioBD != item.GioBD || 
                    lich.GioKT != item.GioKT)
                {
                    if (IsTimeSlotConflict(lich.MaSan, lich.NgayDat.Value, lich.GioBD.Value, lich.GioKT.Value, lich.MaLich))
                    {
                        throw new Exception($"❌ Sân {lich.MaSan} vào khung giờ {lich.GioBD}h - {lich.GioKT}h ngày {lich.NgayDat:dd/MM/yyyy} đã được đặt rồi!\n\n💡 Vui lòng chọn khung giờ khác hoặc sân khác.");
                    }
                }

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
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật lịch: " + ex.Message);
            }
        }

        public bool Delete(string maLich)
        {
            try
            {
                var item = db.LichDats.Find(maLich);

                if (item == null)
                    throw new Exception("❌ Không tìm thấy mã lịch này");

                item.TrangThai = "Đã xóa";
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
                var item = db.LichDats.Find(maLich);
            
                if (item == null) 
                    throw new Exception("❌ Không tìm thấy mã lịch này");

                item.TrangThai = "Đã hủy";
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi hủy sân: " + ex.Message);
            }
        }

        // ===== LẤY DANH SÁCH LỊCH THEO TRẠNG THÁI =====
        public List<LichDat> GetByStatus(string trangThai)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(trangThai))
                    return GetAll();

                var result = db.LichDats
                    .Where(x => x.TrangThai == trangThai && x.TrangThai != "Đã xóa")
                    .OrderByDescending(x => x.NgayDat)
                    .ThenByDescending(x => x.GioBD)
                    .ToList();

                return result;
            }
            catch
            {
                return new List<LichDat>();
            }
        }
    }
}