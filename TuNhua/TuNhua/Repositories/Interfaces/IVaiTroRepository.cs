using TuNhua.Model;

namespace TuNhua.Repositories.Interfaces
{
    public interface IVaiTroRepository
    {
        List<VaiTroDetailVM> GetAll();
        VaiTroDetailVM GetById(Guid id);
        VaiTroDetailVM Add(VaiTroVM model);
        bool Update(Guid id, VaiTroVM model);
        bool Delete(Guid id);
    }
}
