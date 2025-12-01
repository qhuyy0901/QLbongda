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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLich { get; set; }

        public int? MaSan { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        public TimeSpan? GioBD { get; set; }

        public TimeSpan? GioKT { get; set; }

        public decimal? DonGiaThucTe { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual SanBong SanBong { get; set; }

        public virtual User User { get; set; }
    }
}
