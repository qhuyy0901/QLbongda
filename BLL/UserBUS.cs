using DAL;
using System.Linq;

namespace BUS
{
    public class UserBUS
    {
        // Khởi tạo Context kết nối DB
        QuanLySanBongContext db = new QuanLySanBongContext();

        // 1. Hàm đăng nhập (Giữ nguyên của bạn)
        public User Login(string username, string password)
        {
            return db.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        // 2. Hàm kiểm tra xem UserName đã tồn tại chưa
        public bool CheckUsernameExist(string username)
        {
            return db.Users.Any(u => u.UserName == username);
        }

        // 3. Hàm thêm người dùng mới xuống Database
        public void AddUser(User newUser)
        {
            db.Users.Add(newUser);
            db.SaveChanges(); // Lưu thay đổi
        }
    }
}