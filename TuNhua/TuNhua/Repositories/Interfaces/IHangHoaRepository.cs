using TuNhua.Helper;
using TuNhua.Model;

namespace TuNhua.Repositories.Interfaces
{
    public interface IHangHoaRepository
    {
        List<HangHoaDetailVM> GetAll();
        List<HangHoaDetailVM> GetByLoai(Guid loaiId);
        PaginatedList<HangHoaDetailVM> Filter(string? keyword, double? from, double? to, string? sortBy, int page = 1, int pageSize = 6);

        HangHoaDetailVM GetById(Guid id);
        HangHoaVM Add(HangHoaVM model, string? imageUrl);

        bool UpdatePartial(Guid id, HangHoaUpdateVM model, string? imageUrl);

        bool Delete(Guid id);
    }
}
