using System.ComponentModel.DataAnnotations;

namespace TuNhua.Data.Entities
{
    public class VaiTro
    {
        [Key]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string TenVaiTro { get; set; }

        public ICollection<UserDB> UserDBs { get; set; }
    }
}
