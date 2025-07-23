using TuNhua.Data;
using TuNhua.Data.Entities;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Repositories.Implementations
{
    public class VaiTroRepository : IVaiTroRepository
    {
        private readonly MyDbContext _context;

        public VaiTroRepository(MyDbContext context)
        {
            _context = context;
        }
        public VaiTroDetailVM Add(VaiTroVM model)
        {
            var entity = new VaiTro
            {
                RoleId = Guid.NewGuid(),
                TenVaiTro = model.TenVaiTro
            };

            _context.VaiTros.Add(entity);
            _context.SaveChanges();

            return new VaiTroDetailVM
            {
                RoleId = entity.RoleId,
                TenVaiTro = entity.TenVaiTro
            };
        }

        public bool Delete(Guid id)
        {
            var entity = _context.VaiTros.FirstOrDefault(v => v.RoleId == id);
            if (entity == null) return false;

            _context.VaiTros.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public List<VaiTroDetailVM> GetAll()
        {
            return _context.VaiTros.Select(v => new VaiTroDetailVM
            {
                RoleId = v.RoleId,
                TenVaiTro = v.TenVaiTro
            }).ToList();
        }

        public VaiTroDetailVM GetById(Guid id)
        {
            var v = _context.VaiTros.FirstOrDefault(v => v.RoleId == id);
            if (v == null) return null;

            return new VaiTroDetailVM
            {
                RoleId = v.RoleId,
                TenVaiTro = v.TenVaiTro
            };
        }

        public bool Update(Guid id, VaiTroVM model)
        {
            var entity = _context.VaiTros.FirstOrDefault(v => v.RoleId == id);
            if (entity == null) return false;

            entity.TenVaiTro = model.TenVaiTro;
            _context.SaveChanges();
            return true;
        }
    }
}
