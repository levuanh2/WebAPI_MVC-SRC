using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GiohangController : ControllerBase
    {
        private readonly IGioHangRepository _gioHangRepository;

        public GiohangController(IGioHangRepository gioHangRepository) {
            _gioHangRepository= gioHangRepository;
        }
        [HttpGet("nguoidung/{userId}")]
        
        public async Task<IActionResult> LayGioHangTheoUser(Guid userId)
        {
            var gioHang = await _gioHangRepository.LayGioHangTheoUserAsync(userId);
            return Ok(new { success = true, data = gioHang });
        }
        [HttpPost("them")]
        [Authorize]
        public async Task<IActionResult> ThemVaoGio([FromBody] ThemvaoGioHangVm model)
        {
            var result = await _gioHangRepository.ThemVaoGioAsync(model);
            return result ? Ok(new { success = true, message = "Đã thêm vào giỏ hàng" }) : BadRequest(new { success = false, message = "Thêm thất bại" });
        }
        [HttpPut("capnhat")]
        [Authorize]
        public async Task<IActionResult> CapNhatSoLuong([FromBody] CapNhatItemGioHangVM model)
        {
            var result = await _gioHangRepository.CapNhatSoLuongAsync(model);
            return result ? Ok(new { success = true, message = "Cập nhật thành công" }) : BadRequest(new { success = false, message = "Cập nhật thất bại" });
        }
        [HttpDelete("xoa/{userId}/{hangHoaId}")]
        [Authorize]
        public async Task<IActionResult> XoaSanPhamKhoiGio(Guid userId, Guid hangHoaId)
        {
            var result = await _gioHangRepository.XoaSanPhamKhoiGioAsync(userId, hangHoaId);
            return result ? Ok(new { success = true, message = "Đã xóa sản phẩm khỏi giỏ hàng" }) : BadRequest(new { success = false, message = "Xóa thất bại" });
        }
        [HttpDelete("xoatoanbo/{userId}")]
        [Authorize]
        public async Task<IActionResult> XoaToanBoGio(Guid userId)
        {
            var result = await _gioHangRepository.XoaToanBoGioAsync(userId);
            return result ? Ok(new { success = true, message = "Đã xóa toàn bộ giỏ hàng" }) : BadRequest(new { success = false, message = "Xóa thất bại" });
        }
        [HttpGet("tongsoluong/{userId}")]
        public async Task<IActionResult> DemSoLuongSanPham(Guid userId)
        {
            var count = await _gioHangRepository.DemSoLuongSanPhamAsync(userId);
            return Ok(new { success = true, count });
        }

    }
}
