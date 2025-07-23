using TuNhua.Data;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Repositories.Implementations
{
    public class LoaiHangHoaRepository : ILoaiHangHoa
    {
        private readonly MyDbContext _context;

        public LoaiHangHoaRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<LoaiHangHoaVM> GetAll()
        {
            return _context.LoaiHangHoaDBs
                .Select(l => new LoaiHangHoaVM
                {
                    LoaiId=l.LoaiId,
                    TenLoai = l.TenLoai
                })
                .ToList();
        }

        public LoaiHangHoaVM GetById(Guid id)
        {
            var loai = _context.LoaiHangHoaDBs.FirstOrDefault(l => l.LoaiId == id);
            if (loai == null) return null;

            return new LoaiHangHoaVM
            {
                TenLoai = loai.TenLoai
            };
        }

        public LoaiHangHoaVM Add(LoaiHangHoaVM loaiVM)
        {
            var entity = new LoaiHangHoaDB
            {
                LoaiId = Guid.NewGuid(),
                TenLoai = loaiVM.TenLoai
            };

            _context.LoaiHangHoaDBs.Add(entity);
            _context.SaveChanges();

            return new LoaiHangHoaVM
            {
                TenLoai = entity.TenLoai
            };
        }

        public bool Update(Guid id, LoaiHangHoaVM loaiVM)
        {
            var loai = _context.LoaiHangHoaDBs.FirstOrDefault(l => l.LoaiId == id);
            if (loai == null) return false;

            loai.TenLoai = loaiVM.TenLoai;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var loai = _context.LoaiHangHoaDBs.FirstOrDefault(l => l.LoaiId == id);
            if (loai == null) return false;

            _context.LoaiHangHoaDBs.Remove(loai);
            _context.SaveChanges();
            return true;
        }
        public bool Exist(Guid loaiId)
        {
            return _context.LoaiHangHoaDBs.Any(l => l.LoaiId == loaiId);
        }

    }
}
