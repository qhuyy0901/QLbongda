using System.Data.Entity;

namespace DAL
{
    public partial class QuanLySanBongContext : DbContext
    {
        public QuanLySanBongContext()
            : base("name=QuanLySanBongContext")
        {
        }

        public virtual DbSet<Users> Users { get; set; }

        // Các DbSet khác nếu có
        // public virtual DbSet<LichDat> LichDats { get; set; }
    }
}
