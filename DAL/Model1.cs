using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CT_HoaDon_DichVu> CT_HoaDon_DichVu { get; set; }
        public virtual DbSet<DichVu> DichVus { get; set; }
        public virtual DbSet<GiaSan> GiaSans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LichDat> LichDats { get; set; }
        public virtual DbSet<SanBong> SanBongs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CT_HoaDon_DichVu>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DichVu>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DichVu>()
                .HasMany(e => e.CT_HoaDon_DichVu)
                .WithRequired(e => e.DichVu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GiaSan>()
                .Property(e => e.MaSan)
                .IsUnicode(false);

            modelBuilder.Entity<GiaSan>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<GiaSan>()
                .Property(e => e.NgayTrongTuan)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.CT_HoaDon_DichVu)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT_KH)
                .IsUnicode(false);

            modelBuilder.Entity<LichDat>()
                .Property(e => e.MaSan)
                .IsUnicode(false);

            modelBuilder.Entity<LichDat>()
                .Property(e => e.SDT_KH)
                .IsUnicode(false);

            modelBuilder.Entity<LichDat>()
                .Property(e => e.DonGiaThucTe)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LichDat>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.LichDat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanBong>()
                .Property(e => e.MaSan)
                .IsUnicode(false);

            modelBuilder.Entity<SanBong>()
                .HasMany(e => e.GiaSans)
                .WithRequired(e => e.SanBong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanBong>()
                .HasMany(e => e.LichDats)
                .WithRequired(e => e.SanBong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
