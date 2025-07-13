using Microsoft.EntityFrameworkCore;
using TuNhua.Data;
using TuNhua.Data.Entities;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Repositories.Implementations
{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly MyDbContext _context;

        public GioHangRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<GioHangVM> LayGioHangTheoUserAsync(Guid userId)
        {
            var gioHang = await _context.GioHangDBs
                .Include(g => g.ChiTietGioHang)
                .ThenInclude(ct => ct.HangHoa)
                .FirstOrDefaultAsync(g => g.UserId == userId);

            if (gioHang == null)
                return new GioHangVM { Items = new List<GioHangItemVM>() };

            var items = gioHang.ChiTietGioHang.Select(ct => new GioHangItemVM
            {
                MaHangHoa = ct.MaHangHoa,
                TenHangHoa = ct.HangHoa.TenHangHoa,
                DonGia = ct.HangHoa.DonGia,
                SoLuong = ct.SoLuong
            }).ToList();

            return new GioHangVM { Items = items };
        }

        public async Task<bool> ThemVaoGioAsync(ThemvaoGioHangVm model)
        {
            var giohang = await _context.GioHangDBs
                .Include(g => g.ChiTietGioHang)
                .FirstOrDefaultAsync(g => g.UserId == model.UserId);

            if (giohang == null)
            {
                giohang = new GioHangDB
                {
                    MaGioHang = Guid.NewGuid(),
                    UserId = model.UserId,
                    ChiTietGioHang = new List<GioHangChiTietDB>()
                };
                _context.GioHangDBs.Add(giohang);
                await _context.SaveChangesAsync(); // để EF cập nhật MaGioHang
            }

            var existingItem = giohang.ChiTietGioHang
                .FirstOrDefault(ct => ct.MaHangHoa == model.MaHangHoa);

            if (existingItem != null)
            {
                existingItem.SoLuong += model.SoLuong;
            }
            else
            {
                _context.GioHangChiTietDBs.Add(new GioHangChiTietDB
                {
                    MaGioHang = giohang.MaGioHang,
                    MaHangHoa = model.MaHangHoa,
                    SoLuong = model.SoLuong
                });
            }

            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<bool> CapNhatSoLuongAsync(CapNhatItemGioHangVM model)
        {
            var giohang = await _context.GioHangDBs
                .Include(g => g.ChiTietGioHang)
                .FirstOrDefaultAsync(g => g.UserId == model.UserId);

            if (giohang == null) return false;

            var item = giohang.ChiTietGioHang
                .FirstOrDefault(ct => ct.MaHangHoa == model.MaHangHoa);

            if (item == null) return false;

            item.SoLuong = model.SoLuong;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> XoaSanPhamKhoiGioAsync(Guid userId, Guid hangHoaId)
        {
            var giohang = await _context.GioHangDBs
                .Include(g => g.ChiTietGioHang)
                .FirstOrDefaultAsync(g => g.UserId == userId);

            if (giohang == null) return false;

            var item = giohang.ChiTietGioHang
                .FirstOrDefault(ct => ct.MaHangHoa == hangHoaId);

            if (item == null) return false;

            giohang.ChiTietGioHang.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> XoaToanBoGioAsync(Guid userId)
        {
            var giohang = await _context.GioHangDBs
                .Include(g => g.ChiTietGioHang)
                .FirstOrDefaultAsync(g => g.UserId == userId);

            if (giohang == null) return false;

            _context.GioHangChiTietDBs.RemoveRange(giohang.ChiTietGioHang);
            _context.GioHangDBs.Remove(giohang);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SanPhamDaTonTaiAsync(Guid userId, Guid hangHoaId)
        {
            var giohang = await _context.GioHangDBs
                .Include(g => g.ChiTietGioHang)
                .FirstOrDefaultAsync(g => g.UserId == userId);

            return giohang?.ChiTietGioHang.Any(ct => ct.MaHangHoa == hangHoaId) ?? false;
        }

        public async Task<int> DemSoLuongSanPhamAsync(Guid userId)
        {
            var gioHang = await _context.GioHangDBs
                .Include(g => g.ChiTietGioHang)
                .FirstOrDefaultAsync(g => g.UserId == userId);

            return gioHang?.ChiTietGioHang.Sum(ct => ct.SoLuong) ?? 0;
        }

       
    }
}
