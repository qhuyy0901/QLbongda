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
        [Key]
        [StringLength(20)]
        public string MaLich { get; set; }

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

        // ===== PROPERTY ?O ?? HI?N TH? TRONG COMBOBOX =====
        [NotMapped]
        public string ThongTinLich { get; set; }
    }
}
