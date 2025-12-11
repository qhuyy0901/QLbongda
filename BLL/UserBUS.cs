using DAL;
using System.Linq;

namespace BUS
{
    public class UserBUS
    {
        // SỬA: Dùng Model1 thay vì QuanLySanBongContext
        Model1 db = new Model1();

        public User Login(string username, string password)
        {
            // Tìm user trong DB
            return db.Users.FirstOrDefault(u => u.ID == username && u.Password == password);
        }

        public void AddUser(User newUser)
        {
            db.Users.Add(newUser);
            db.SaveChanges();
        }
    }
}