using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuNhua.Data;
using TuNhua.Model;

namespace TuNhua.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiHangHoa : ControllerBase
    {
        private readonly MyDbContext _db;
        public LoaiHangHoa(MyDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var dsLoai = _db.LoaiHangHoaDBs.ToList();
            return Ok(dsLoai);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id) {
            var dsLoai = _db.LoaiHangHoaDBs.SingleOrDefault(loai => loai.LoaiId == Guid.Parse(id));
            if (dsLoai == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                success = true,
                Data = dsLoai
            });
        }
        [HttpPost]
        public IActionResult ThemMoi(LoaiHangHoaVM loaiHangHoaVM)
        {
            try
            {
                var loai = new LoaiHangHoaDB
                {
                    LoaiId=Guid.NewGuid(),
                    TenLoai = loaiHangHoaVM.TenLoai
                };
                _db.Add(loai);
                _db.SaveChanges();
                return Ok(new
                {
                    success = true,
                    Data = loai
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{id}")]
        public IActionResult CapNhatLoai([FromBody] LoaiHangHoaVM loaiVM, string id)
        {
            var loai = _db.LoaiHangHoaDBs.SingleOrDefault(l => l.LoaiId == Guid.Parse(id));
            if (loai == null)
                return NotFound();

            try
            {
                loai.TenLoai = loaiVM.TenLoai;
                _db.SaveChanges();

                return Ok(new
                {
                    success = true,
                    data = loai
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult XoaLoai( string id)
        {
            var Loai=_db.LoaiHangHoaDBs.SingleOrDefault(_ => _.LoaiId ==Guid.Parse( id));
            if (Loai == null)
            {
                return NotFound();
            }
            _db.LoaiHangHoaDBs.Remove(Loai);
            _db.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Xóa thành công"
            });
        }
    }
}
