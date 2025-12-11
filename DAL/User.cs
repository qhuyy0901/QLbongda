namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(100)]
        public string TenNguoiDung { get; set; }
    }
}
