using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TuNhua.Data;
using TuNhua.Data.Entities;
using TuNhua.Helper;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Repositories.Implementations
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly MyDbContext _context;
        private readonly JwtHelper _jwtHelper;
        

        public NguoiDungRepository(MyDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }
        public bool CheckUsernameExit(string username)
        {
            return _context.UserDBs.Any(u => u.Username == username);
        }

        public UserInfoVM GetUserInfo(string username)
        {
            var user = _context.UserDBs.FirstOrDefault(u => u.Username == username);
            if (user == null) return null;

            var role = _context.VaiTros.FirstOrDefault(r => r.RoleId == user.RoleId);

            return new UserInfoVM
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                VaiTro = role?.TenVaiTro ?? "Unknown"
            };
        }

        public async Task<List<UserInfoVM>> LayTatCaNguoiDungAsync()
        {
            var users = await _context.UserDBs
                .Include(u => u.vaiTro)
                .Select(u => new UserInfoVM
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    VaiTro = u.vaiTro != null ? u.vaiTro.TenVaiTro : "Unknown"
                })
                .ToListAsync();

            return users;
        }


        public string Login(LoginVM loginVM, out RefreshToken refreshToken)
        {
            refreshToken = null;

            var user = _context.UserDBs.FirstOrDefault(u => u.Username == loginVM.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password))
            {
                return null;
            }

            var role = _context.VaiTros.FirstOrDefault(r => r.RoleId == user.RoleId)?.TenVaiTro ?? "User";

            var (accessToken, newRefreshToken) = _jwtHelper.GenerateTokens(user, role);

            
            _context.RefreshTokens.Add(newRefreshToken);
            _context.SaveChanges();

            refreshToken = newRefreshToken;
            return accessToken;
        }

        public async Task<TokenResponse> RefreshTokenAsync(TokenRequest tokenRequest)
        {
            try
            {
                var principal = _jwtHelper.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
                if (principal == null) return null;

                var username = principal.Identity.Name;
                var jwtId = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

                var user = await _context.UserDBs.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null) return null;

                var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(r =>
                    r.Token == tokenRequest.RefreshToken &&
                    r.JwtId == jwtId &&
                    r.UserId == user.UserId
                );

                if (storedToken == null || storedToken.IsUsed || storedToken.IsRevoked || storedToken.ExpiredAt <= DateTime.UtcNow)
                    return null;

                // Đánh dấu token cũ đã dùng
                storedToken.IsUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                var role = await _context.VaiTros
                    .Where(r => r.RoleId == user.RoleId)
                    .Select(r => r.TenVaiTro)
                    .FirstOrDefaultAsync();

                var (newAccessToken, newRefreshToken) = _jwtHelper.GenerateTokens(user, role);
                await _context.RefreshTokens.AddAsync(newRefreshToken);
                await _context.SaveChangesAsync();

                return new TokenResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken.Token,
                    Expires = newRefreshToken.ExpiredAt
                };
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Refresh Token Failed: " + ex.Message);
                return null;
            }
        }

        public bool Register(RegisterVM registerVM)
        {
            if(CheckUsernameExit(registerVM.Username)) return false;
            var user = new UserDB
            {
                UserId = new Guid(),
                Username = registerVM.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password),
                Email = registerVM.Email,
                RoleId = registerVM.RoleId,
            };
            _context.UserDBs.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}
