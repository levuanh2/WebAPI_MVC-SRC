namespace TuNhua.Data.Entities
{
    public class GioHangDB
    {
        public Guid MaGioHang { get; set; }
        public Guid UserId { get; set; }

        public UserDB User { get; set; }
        public ICollection<GioHangChiTietDB> ChiTietGioHang { get; set; }
    }

    public class GioHangChiTietDB
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MaGioHang { get; set; }
        public GioHangDB GioHang { get; set; }

        public Guid MaHangHoa { get; set; }
        public HangHoaDB HangHoa { get; set; }

        public int SoLuong { get; set; }
    }

}
