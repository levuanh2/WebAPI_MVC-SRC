namespace TuNhua.Model
{
    public class TaoDonHangVM
    {
        public string DiaChiGiao { get; set; }
        public string SoDienThoai { get; set; }
        public Guid UserId { get; set; }

        public List<HangTrongDonVM> DanhSachHang { get; set; }
    }

    public class HangTrongDonVM
    {
        public Guid MaHangHoa { get; set; }
        public int SoLuong { get; set; }
    }
    public class DonHangChiTietVM
    {
        public Guid MaDonHang { get; set; }
        public DateTime NgayDat { get; set; }
        public string DiaChiGiao { get; set; }
        public string SoDienThoai { get; set; }
        public decimal TongTien { get; set; }

        public List<ChiTietVM> ChiTiet { get; set; }
    }

    public class ChiTietVM
    {
        public string TenHangHoa { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }


}
