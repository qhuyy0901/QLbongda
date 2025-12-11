using DAL;
using System.Linq;

namespace BUS
{
    public class UserBUS
    {
        // SỬA: Dùng Model1 thay vì QuanLySanBongContext
        Model1 db = new Model1();

        public Users Login(string username, string password)
        {
            // Tìm user trong DB
            return db.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public void AddUser(Users newUser)
        {
            db.Users.Add(newUser);
            db.SaveChanges();
        }
    }
}