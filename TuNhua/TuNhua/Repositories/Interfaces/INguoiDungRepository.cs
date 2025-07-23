using TuNhua.Data.Entities;
using TuNhua.Model;

namespace TuNhua.Repositories.Interfaces
{
    public interface INguoiDungRepository
    {
        bool Register(RegisterVM registerVM);
        string Login(LoginVM loginVM, out RefreshToken refreshToken);

        UserInfoVM GetUserInfo(string username);
        bool CheckUsernameExit(string username);
        Task<TokenResponse> RefreshTokenAsync(TokenRequest request);
        Task<List<UserInfoVM>> LayTatCaNguoiDungAsync();

    }
}
