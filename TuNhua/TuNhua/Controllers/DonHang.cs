using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuNhua.Data;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IDonHangRepository _donHangRepository;

        public DonHangController(MyDbContext context, IDonHangRepository donHangRepository)
        {
            _context = context;
            _donHangRepository = donHangRepository;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> TaoDonHang([FromBody] TaoDonHangVM donHang)
        {
            var result = await _donHangRepository.TaoDonHangAsync(donHang);
            return result ? Ok(new { success = true }) : BadRequest(new { success = false });
        }
        [HttpGet("nguoidung/{userId}")]
        [Authorize]
        public async Task<IActionResult> LayDonHangTheoUser(Guid userId)
        {
            var donHangs = await _donHangRepository.LayDonHangTheoUser(userId);
            return Ok(new { success = true, data = donHangs });
        }

        [HttpGet("tatca")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LayTatCaDonHang()
        {
            var donHangs = await _donHangRepository.LayTatCaDonHang();
            return Ok(new { success = true, data = donHangs });
        }
        [HttpPost("pheduyet/{donHangId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PheduyetDonHang(Guid donHangId)
        {
            var result = await _donHangRepository.PheduyetDonHangAsync(donHangId);
            return result ? Ok(new { success = true }) : BadRequest(new { success = false });
        }
    }
}
