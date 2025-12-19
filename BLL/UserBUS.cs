using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class UserBUS
    {
        private Model1 db = new Model1();

        // ===== LOGIN =====
        public User Login(string username, string password)
        {
            try
            {
                // ===== LẤY USER TỪ DATABASE =====
                var user = db.Users.FirstOrDefault(u => u.ID == username);
                
                if (user == null)
                    return null;

                // ===== CỐ GẮNG GIẢI MÃ PASSWORD =====
                string decryptedPassword = MaHoaASCII.DecryptPassword(user.Password);

                // ===== SO SÁNH VỚI PASSWORD NHẬP VÀO =====
                if (decryptedPassword == password)
                    return user;

                // ===== NẾU GIẢI MÃ THẤT BẠI (PASSWORD CŨ PLAINTEXT), SO SÁNH TRỰC TIẾP =====
                if (user.Password == password)
                    return user;

                return null;
            }
            catch
            {
                return null;
            }
        }

        // ===== GET ALL USERS =====
        public List<User> GetAll()
        {
            try
            {
                return db.Users.ToList();
            }
            catch
            {
                return new List<User>();
            }
        }

        // ===== GET USER BY ID =====
        public User GetByID(string id)
        {
            try
            {
                return db.Users.FirstOrDefault(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }

        // ===== ADD USER =====
        public bool Insert(User newUser)
        {
            try
            {
                if (newUser == null)
                    throw new Exception("Tài khoản không hợp lệ");

                if (string.IsNullOrWhiteSpace(newUser.ID))
                    throw new Exception("ID không được để trống");

                if (string.IsNullOrWhiteSpace(newUser.Password))
                    throw new Exception("Password không được để trống");

                // Kiểm tra ID đã tồn tại
                var existingUser = db.Users.FirstOrDefault(u => u.ID == newUser.ID);
                if (existingUser != null)
                    throw new Exception($"ID '{newUser.ID}' đã tồn tại");

                db.Users.Add(newUser);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert User lỗi: " + ex.Message);
            }
        }

        // ===== UPDATE USER =====
        public bool Update(User user)
        {
            try
            {
                if (user == null)
                    throw new Exception("Tài khoản không hợp lệ");

                if (string.IsNullOrWhiteSpace(user.ID))
                    throw new Exception("ID không được để trống");

                var item = db.Users.Find(user.ID);
                if (item == null)
                    throw new Exception($"Không tìm thấy tài khoản '{user.ID}'");

                // Cập nhật thông tin
                item.Password = user.Password;
                item.Role = user.Role;
                item.TenNguoiDung = user.TenNguoiDung;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Update User lỗi: " + ex.Message);
            }
        }

        // ===== DELETE USER =====
        public bool Delete(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("ID không hợp lệ");

                var item = db.Users.Find(id);
                if (item == null)
                    throw new Exception($"Không tìm thấy tài khoản '{id}'");

                db.Users.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete User lỗi: " + ex.Message);
            }
        }

        // ===== SEARCH USER =====
        public List<User> Search(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return GetAll();

                keyword = keyword.ToLower().Trim();

                return db.Users.Where(u =>
                    u.ID.ToLower().Contains(keyword) ||
                    u.Role.ToLower().Contains(keyword) ||
                    u.TenNguoiDung.ToLower().Contains(keyword)
                ).ToList();
            }
            catch
            {
                return new List<User>();
            }
        }
    }
}