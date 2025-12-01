using DAL;
using System.Linq;

namespace BUS
{
    public class UserBLL
    {
        QuanLySanBongContext db = new QuanLySanBongContext();

        public User Login(string username, string password)
        {
            return db.Users.FirstOrDefault(u =>
                u.UserName == username &&
                u.Password == password
            );
        }
    }
}
