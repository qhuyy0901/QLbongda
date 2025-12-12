namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_HoaDon_DichVu
    {
        [Key]
        [StringLength(20)]
        public string MaCT { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHD { get; set; }

        [Required]
        [StringLength(20)]
        public string MaDV { get; set; }

        public int? SoLuong { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual DichVu DichVu { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
