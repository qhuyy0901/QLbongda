namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            CT_HoaDon_DichVu = new HashSet<CT_HoaDon_DichVu>();
        }

        [Key]
        public int MaHD { get; set; }

        public int MaLich { get; set; }

        public decimal? TongTien { get; set; }

        public DateTime? ThoiGianThanhToan { get; set; }

        [StringLength(50)]
        public string HinhThucTT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon_DichVu> CT_HoaDon_DichVu { get; set; }

        public virtual LichDat LichDat { get; set; }
    }
}
