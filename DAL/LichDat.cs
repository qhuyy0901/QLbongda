namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichDat")]
    public partial class LichDat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LichDat()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        public int MaLich { get; set; }

        [Required]
        [StringLength(20)]
        public string MaSan { get; set; }

        [StringLength(20)]
        public string SDT_KH { get; set; }

        [StringLength(100)]
        public string TenKH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        public int? GioBD { get; set; }

        public int? GioKT { get; set; }

        public decimal? DonGiaThucTe { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual SanBong SanBong { get; set; }
    }
}
