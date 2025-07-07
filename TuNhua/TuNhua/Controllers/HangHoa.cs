using Microsoft.AspNetCore.Mvc;
using TuNhua.Data;
using TuNhua.Model;

namespace TuNhua.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly MyDbContext _db;

        public HangHoaController(MyDbContext db)
        {
            _db = db;
        }

        
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _db.HangHoaDBs.ToList();
            return Ok(result);
        }

        
        [HttpGet("theoloai/{loaiId}")]
        public IActionResult GetByLoai(Guid loaiId)
        {
            var hangHoas = _db.HangHoaDBs
                .Where(h => h.LoaiId == loaiId)
                .ToList();

            if (hangHoas.Count == 0)
            {
                return NotFound(new { success = false, message = "Không có sản phẩm nào thuộc loại này" });
            }

            return Ok(new
            {
                success = true,
                data = hangHoas
            });
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var hangHoa = _db.HangHoaDBs.FirstOrDefault(hh => hh.MaHangHoa == id);
            if (hangHoa == null) return NotFound();

            return Ok(new
            {
                success = true,
                data = hangHoa
            });
        }

        
        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hangHoa = new HangHoaDB
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia,
                Mota = hangHoaVM.Mota,
                Soluong = hangHoaVM.Soluong,
                LoaiId = hangHoaVM.LoaiId
            };

            _db.HangHoaDBs.Add(hangHoa);
            _db.SaveChanges();

            return Ok(new
            {
                success = true,
                data = hangHoa
            });
        }

        
        [HttpPut("{id}")]
        public IActionResult Edit(Guid id, HangHoaVM hangHoaVM)
        {
            var hang = _db.HangHoaDBs.FirstOrDefault(hh => hh.MaHangHoa == id);
            if (hang == null) return NotFound();

            hang.TenHangHoa = hangHoaVM.TenHangHoa;
            hang.Mota = hangHoaVM.Mota;
            hang.DonGia = hangHoaVM.DonGia;
            hang.Soluong = hangHoaVM.Soluong;
            hang.LoaiId = hangHoaVM.LoaiId;

            _db.SaveChanges();

            return Ok(new
            {
                success = true,
                data = hang
            });
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var hang = _db.HangHoaDBs.FirstOrDefault(hh => hh.MaHangHoa == id);
            if (hang == null) return NotFound();

            _db.HangHoaDBs.Remove(hang);
            _db.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Xoá thành công"
            });
        }
    }
}
