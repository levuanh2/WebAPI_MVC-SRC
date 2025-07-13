using System.ComponentModel.DataAnnotations;
using TuNhua.Data.Entities;

public class LoaiHangHoaDB
{
    [Key]
    public Guid LoaiId { get; set; }

    [Required]
    public string TenLoai { get; set; }

    public ICollection<HangHoaDB> HangHoas { get; set; }
}
