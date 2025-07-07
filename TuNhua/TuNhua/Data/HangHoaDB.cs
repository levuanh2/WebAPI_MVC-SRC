using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuNhua.Data
{
    [Table("Hanghoa")]
    public class HangHoaDB
    {
        [Key]
        [Required]
        public Guid MaHangHoa {  get; set; }
        [Required]
        [StringLength(100)]

        public string TenHangHoa { get; set; }
        [Range(0, double.MaxValue)]
        public decimal DonGia { get; set; }

        public string Mota { get; set; }

        [Range(0, int.MaxValue)]
        public int Soluong { get; set; }
        [Required]
        public Guid LoaiId { get; set; }

        [ForeignKey(nameof(LoaiId))]
        public LoaiHangHoaDB Loai { get; set; }
        public ICollection<ChiTietDonHangDB> ChiTietDonHangs { get; set; }
    }
}
