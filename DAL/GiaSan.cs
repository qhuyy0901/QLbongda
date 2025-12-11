namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaSan")]
    public partial class GiaSan
    {
        [Key]
        public int MaGia { get; set; }

        [Required]
        [StringLength(20)]
        public string MaSan { get; set; }

        public int? GioBD { get; set; }

        public int? GioKT { get; set; }

        public decimal? DonGia { get; set; }

        [StringLength(20)]
        public string NgayTrongTuan { get; set; }

        public virtual SanBong SanBong { get; set; }
    }
}
