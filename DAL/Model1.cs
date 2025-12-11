using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DAL
{
    public partial class Model1 : DbContext
    {
        public Model1() : base("name=QuanLySanBongContext")
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<LichDat> LichDats { get; set; }
        public virtual DbSet<SanBong> SanBongs { get; set; }
        public virtual DbSet<DichVu> DichVus { get; set; }
        public virtual DbSet<GiaSan> GiaSans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<CT_HoaDon_DichVu> CT_HoaDon_DichVu { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Cấu hình số thập phân (Money)
            modelBuilder.Entity<DichVu>().Property(e => e.DonGia).HasPrecision(18, 0);
            modelBuilder.Entity<GiaSan>().Property(e => e.DonGia).HasPrecision(18, 0);
            modelBuilder.Entity<HoaDon>().Property(e => e.TongTien).HasPrecision(18, 0);
            modelBuilder.Entity<LichDat>().Property(e => e.DonGiaThucTe).HasPrecision(18, 0);
            modelBuilder.Entity<CT_HoaDon_DichVu>().Property(e => e.ThanhTien).HasPrecision(18, 0);
        }
    }
}