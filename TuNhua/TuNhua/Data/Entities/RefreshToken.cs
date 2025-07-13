using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuNhua.Data.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string JwtId { get; set; }

        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }

        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }

        // Quan hệ với User
        public Guid UserId { get; set; }
        public UserDB User { get; set; }
    }
}
