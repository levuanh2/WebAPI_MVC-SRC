using TuNhua.Model;

namespace TuNhua.Repositories.Interfaces
{
    public interface ILoaiHangHoa
    {
        List<LoaiHangHoaVM> GetAll();

       
        LoaiHangHoaVM GetById(Guid id);
        LoaiHangHoaVM Add(LoaiHangHoaVM loai);
        bool Update(Guid id, LoaiHangHoaVM loai);
        bool Delete(Guid id);
        bool Exist(Guid loaiId);

    }
}
