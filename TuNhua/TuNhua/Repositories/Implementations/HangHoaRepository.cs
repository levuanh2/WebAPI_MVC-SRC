using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TuNhua.Data;
using TuNhua.Data.Entities;
using TuNhua.Helper;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Repositories.Implementations
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly MyDbContext _context;

        public HangHoaRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<HangHoaDetailVM> GetAll()
        {
            return _context.HangHoaDBs
                .Select(h => new HangHoaDetailVM
                {
                    MaHangHoa = h.MaHangHoa,
                    TenHangHoa = h.TenHangHoa,
                    DonGia = h.DonGia,
                    Mota = h.Mota,
                    Soluong = h.Soluong,
                    LoaiId = h.LoaiId,
                    TenLoai = h.Loai.TenLoai,
                    HinhAnh =  h.HinhAnh
                })
                .ToList();
        }


        public List<HangHoaDetailVM> GetByLoai(Guid loaiId)
        {
            return _context.HangHoaDBs
                .Where(h => h.LoaiId == loaiId)
                .Select(h => new HangHoaDetailVM
                {
                    MaHangHoa = h.MaHangHoa,
                    TenHangHoa = h.TenHangHoa,
                    DonGia = h.DonGia,
                    Mota = h.Mota,
                    Soluong = h.Soluong,
                    LoaiId = h.LoaiId,
                    TenLoai = h.Loai.TenLoai,
                    HinhAnh = h.HinhAnh
                })
                .ToList();
        }
        public PaginatedList<HangHoaDetailVM> Filter(string? keyword, double? from, double? to, string? sortBy, int page = 1, int pageSize = 6)
        {
            var query = _context.HangHoaDBs.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(h => h.TenHangHoa.Contains(keyword));
            }

            if (from.HasValue)
            {
                query = query.Where(h => h.DonGia >= (decimal)from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(h => h.DonGia <= (decimal)to.Value);
            }

            query = sortBy?.ToLower() switch
            {
                "desc" => query.OrderByDescending(h => h.DonGia),
                "asc" => query.OrderBy(h => h.DonGia),
                _ => query.OrderBy(h => h.TenHangHoa)
            };

            var projected = query.Select(h => new HangHoaDetailVM
            {
                MaHangHoa = h.MaHangHoa,
                TenHangHoa = h.TenHangHoa,
                DonGia = h.DonGia,
                Mota = h.Mota,
                Soluong = h.Soluong,
                LoaiId = h.LoaiId,
                TenLoai = h.Loai.TenLoai
            });

            return PaginatedList<HangHoaDetailVM>.Create(projected, page, pageSize);
        }
        public HangHoaDetailVM GetById(Guid id)
        {
            var h = _context.HangHoaDBs
            .Include(hh => hh.Loai)
            .FirstOrDefault(hh => hh.MaHangHoa == id);
            if (h == null) return null;

            return new HangHoaDetailVM
            {
                MaHangHoa = h.MaHangHoa,
                TenHangHoa = h.TenHangHoa,
                DonGia = h.DonGia,
                Mota = h.Mota,
                Soluong = h.Soluong,
                LoaiId = h.LoaiId,
                TenLoai = h.Loai.TenLoai,
                HinhAnh = h.HinhAnh
            };
        }

        public HangHoaVM Add(HangHoaVM model, string? imageUrl)
        {
            var entity = new HangHoaDB
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = model.TenHangHoa,
                DonGia = model.DonGia,
                Mota = model.Mota,
                Soluong = model.Soluong,
                LoaiId = model.LoaiId,
                HinhAnh = imageUrl
            };

            _context.HangHoaDBs.Add(entity);
            _context.SaveChanges();

            return model;
        }


        public bool UpdatePartial(Guid id, HangHoaUpdateVM model, string? imageUrl)
        {
            var entity = _context.HangHoaDBs.FirstOrDefault(h => h.MaHangHoa == id);
            if (entity == null) return false;

            if (!string.IsNullOrEmpty(model.TenHangHoa))
                entity.TenHangHoa = model.TenHangHoa;

            if (!string.IsNullOrEmpty(model.Mota))
                entity.Mota = model.Mota;

            if (model.DonGia.HasValue)
                entity.DonGia = model.DonGia.Value;

            if (model.Soluong.HasValue)
                entity.Soluong = model.Soluong.Value;

            if (model.LoaiId.HasValue)
                entity.LoaiId = model.LoaiId.Value;

            if (!string.IsNullOrEmpty(imageUrl))
                entity.HinhAnh = imageUrl;

            _context.SaveChanges();
            return true;
        }


        public bool Delete(Guid id)
        {
            var entity = _context.HangHoaDBs.FirstOrDefault(h => h.MaHangHoa == id);
            if (entity == null) return false;

            _context.HangHoaDBs.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
