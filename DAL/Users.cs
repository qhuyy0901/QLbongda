using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("Users")]
    public partial class Users
    {
        public Users()
        {
            LichDats = new HashSet<LichDat>();
        }

        [Key]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        [StringLength(100)]
        public string TenNguoiDung { get; set; }


        [StringLength(255)]
        public string GhiChu { get; set; }

        public virtual ICollection<LichDat> LichDats { get; set; }
    }
}