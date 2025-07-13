using Microsoft.AspNetCore.Mvc;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        private readonly IVaiTroRepository _context;

        public VaiTroController(IVaiTroRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _context.GetAll();
            return Ok(new { success = true, data = result });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _context.GetById(id);
            if (result == null)
                return NotFound(new { success = false, message = "Vai trò không tồn tại" });

            return Ok(new { success = true, data = result });
        }

        [HttpPost]
        public IActionResult Add(VaiTroVM model)
        {
            var result = _context.Add(model);
            return Ok(new { success = true, data = result });
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, VaiTroVM model)
        {
            var success = _context.Update(id, model);
            if (!success)
                return NotFound(new { success = false, message = "Không tìm thấy vai trò" });

            return Ok(new { success = true, message = "Cập nhật thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var success = _context.Delete(id);
            if (!success)
                return NotFound(new { success = false, message = "Không tìm thấy vai trò" });

            return Ok(new { success = true, message = "Xóa thành công" });
        }
    }
}
