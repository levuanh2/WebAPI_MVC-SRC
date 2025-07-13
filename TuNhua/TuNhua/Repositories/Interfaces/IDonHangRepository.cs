using TuNhua.Model;

namespace TuNhua.Repositories.Interfaces
{
    public interface IDonHangRepository
    {
        Task<bool> TaoDonHangAsync(TaoDonHangVM donHangVM);
        Task <List<DonHangChiTietVM>> LayDonHangTheoUser(Guid userId);
        Task<List<DonHangChiTietVM>> LayTatCaDonHang();
        Task<bool> PheduyetDonHangAsync(Guid donHangId);
    }
}
