namespace TuNhua.Model
{
    public class GioHangVM
    {
        public List<GioHangItemVM> Items { get; set; }
        public decimal TongTien => Items?.Sum(x => x.ThanhTien) ?? 0;
    }
    public class ThemvaoGioHangVm
    {
        public Guid UserId { get; set; }
        public Guid MaHangHoa { get; set; }
        public int SoLuong { get; set; }
    }
    public class CapNhatItemGioHangVM
    {
        public Guid UserId { get; set; }
        public Guid MaHangHoa { get; set; }
        public int SoLuong { get; set; }
    }
    public class GioHangItemVM
    {
        public Guid MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
