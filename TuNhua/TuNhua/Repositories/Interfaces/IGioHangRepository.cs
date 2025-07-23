using TuNhua.Data;
using TuNhua.Model;
using static TuNhua.Model.GioHangVM;
namespace TuNhua.Repositories.Interfaces
{
    public interface IGioHangRepository
    {
        Task<GioHangVM> LayGioHangTheoUserAsync(Guid userId);

        Task<bool> ThemVaoGioAsync(ThemvaoGioHangVm model);
        Task<bool> CapNhatSoLuongAsync(CapNhatItemGioHangVM model);
        Task<bool> XoaSanPhamKhoiGioAsync(Guid userId, Guid hangHoaId);
        Task<bool> XoaToanBoGioAsync(Guid userId);
        Task<bool> SanPhamDaTonTaiAsync(Guid userId, Guid hangHoaId);
        Task<int> DemSoLuongSanPhamAsync(Guid userId);
    }
}
