namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanBong")]
    public partial class SanBong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanBong()
        {
            GiaSans = new HashSet<GiaSan>();
            LichDats = new HashSet<LichDat>();
        }

        [Key]
        [StringLength(20)]
        public string MaSan { get; set; }

        [Required]
        [StringLength(100)]
        public string TenSan { get; set; }

        [StringLength(50)]
        public string LoaiSan { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiaSan> GiaSans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichDat> LichDats { get; set; }
    }
}
