using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using TuNhua.Data.Entities;

public class UserDB
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }

    public VaiTro vaiTro { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }

    public ICollection<DonHangDB> DonHangDBs { get; set; }
}
