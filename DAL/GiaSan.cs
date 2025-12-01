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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaGia { get; set; }

        public int? MaSan { get; set; }

        public TimeSpan? GioBD { get; set; }

        public TimeSpan? GioKT { get; set; }

        public decimal? DonGia { get; set; }

        public int? NgayTrongTuan { get; set; }

        public virtual SanBong SanBong { get; set; }
    }
}
