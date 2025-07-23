using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DonHangDB
{
    public enum TinhTrangDonhang
    {
        New = 0,
        Payment = 1,
        Complete = 2,
        Cancel = -1
    }

    [Key]
    public Guid MaDonHang { get; set; }

    public DateTime NgayDat { get; set; } = DateTime.Now;

    [Required]
    [StringLength(200)]
    public string DiaChiGiao { get; set; }

    [Required]
    [Phone]
    [RegularExpression(@"^(0|\+84)[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
    public string SoDienThoai { get; set; }

    public decimal TongTien { get; set; }

    public TinhTrangDonhang TinhTrang { get; set; } = TinhTrangDonhang.New;

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public UserDB UserBD { get; set; }

    public ICollection<ChiTietDonHangDB> ChiTietDonHangDBs { get; set; }
}
