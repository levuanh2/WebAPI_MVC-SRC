using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IConfiguration _configuration;

        public NguoiDungController(INguoiDungRepository nguoiDungRepository, IConfiguration configuration)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _configuration = configuration;
        }

        [HttpPost("DangKy")]
        public IActionResult DangKy(RegisterVM registerVM)
        {
            if (_nguoiDungRepository.CheckUsernameExit(registerVM.Username))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Username đã tồn tại"
                });
            }

            var result = _nguoiDungRepository.Register(registerVM);
            if (!result)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Đăng ký thất bại"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Đăng ký thành công"
            });
        }

        [HttpPost("DangNhap")]
        public IActionResult DangNhap(LoginVM loginVM)
        {
            var token = _nguoiDungRepository.Login(loginVM, out var refreshToken);

            if (token == null)
                return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu" });

            return Ok(new
            {
                success = true,
                accessToken = token,
                refreshToken = refreshToken.Token,       
                refreshTokenExpiry = refreshToken.ExpiredAt
            });
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request)
        {
            var response = await _nguoiDungRepository.RefreshTokenAsync(request);
            if (response == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Refresh token không hợp lệ hoặc đã bị thu hồi"
                });
            }

            return Ok(new
            {
                success = true,
                accessToken = response.AccessToken,
                refreshToken = response.RefreshToken,
                expires = response.Expires
            });
        }

        [HttpGet("tatca")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> LayTatCaNguoiDung()
        {
            var users = await _nguoiDungRepository.LayTatCaNguoiDungAsync();
            return Ok(new
            {
                success = true,
                data = users
            });
        }

    }
}
