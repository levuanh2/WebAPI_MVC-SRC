using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TuNhua.Data;

public class ChiTietDonHangDB
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid MaDonHang { get; set; }

    [ForeignKey(nameof(MaDonHang))]
    public DonHangDB DonHang { get; set; }

    [Required]
    public Guid MaHangHoa { get; set; }

    [ForeignKey(nameof(MaHangHoa))]
    public HangHoaDB HangHoa { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
    public int SoLuong { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Đơn giá không hợp lệ")]
    public decimal DonGia { get; set; }

    [NotMapped]
    public decimal ThanhTien => DonGia * SoLuong;
}
