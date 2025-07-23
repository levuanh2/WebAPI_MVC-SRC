using System.ComponentModel.DataAnnotations;

namespace TuNhua.Model
{
    public class HangHoaVM
    {
        public string TenHangHoa { get; set; }
        public string Mota { get; set; }
        public decimal DonGia { get; set; }
        public int Soluong { get; set; }
        public Guid LoaiId { get; set; }
    }

    public class HangHoaDetailVM : HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
        public string TenLoai { get; set; }
    }

    public class HangHoa : HangHoaVM
    {
        [Key]
        public Guid MaHanghoa { get; set; }
    }
}
