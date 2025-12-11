namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DichVu")]
    public partial class DichVu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DichVu()
        {
            CT_HoaDon_DichVu = new HashSet<CT_HoaDon_DichVu>();
        }

        [Key]
        public int MaDV { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDV { get; set; }

        public decimal? DonGia { get; set; }

        [StringLength(20)]
        public string DonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon_DichVu> CT_HoaDon_DichVu { get; set; }
    }
}
