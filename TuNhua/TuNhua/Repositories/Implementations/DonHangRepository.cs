using Microsoft.EntityFrameworkCore;
using TuNhua.Data;
using TuNhua.Model;
using TuNhua.Repositories.Interfaces;

namespace TuNhua.Repositories.Implementations
{
    public class DonHangRepository : IDonHangRepository
    {
        private MyDbContext _context;

        public DonHangRepository(MyDbContext context) {
            _context=context;
        }
        public async Task<List<DonHangChiTietVM>> LayDonHangTheoUser(Guid userId)
        {
            var donhangs = await _context.DonHangDBs
                .Where(dh => dh.UserId == userId)
                .Include(dh => dh.ChiTietDonHangDBs)
                .ThenInclude(ct => ct.HangHoa)
                .ToListAsync();
            return donhangs.Select(d => new DonHangChiTietVM
            {
                MaDonHang = d.MaDonHang,
                NgayDat = d.NgayDat,
                DiaChiGiao = d.DiaChiGiao,
                SoDienThoai = d.SoDienThoai,
                TongTien = d.TongTien,
                ChiTiet = d.ChiTietDonHangDBs.Select(c => new ChiTietVM
                {
                    TenHangHoa = c.HangHoa.TenHangHoa,
                    SoLuong = c.SoLuong,
                    DonGia = c.DonGia
                }).ToList()
            }).ToList();
        }

        public async Task<List<DonHangChiTietVM>> LayTatCaDonHang()
        {
            var donHangs = await _context.DonHangDBs
                .Include(d => d.ChiTietDonHangDBs)
                    .ThenInclude(c => c.HangHoa)
                .ToListAsync();

            return donHangs.Select(d => new DonHangChiTietVM
            {
                MaDonHang = d.MaDonHang,
                NgayDat = d.NgayDat,
                DiaChiGiao = d.DiaChiGiao,
                SoDienThoai = d.SoDienThoai,
                TongTien = d.TongTien,
                ChiTiet = d.ChiTietDonHangDBs.Select(c => new ChiTietVM
                {
                    TenHangHoa = c.HangHoa.TenHangHoa,
                    SoLuong = c.SoLuong,
                    DonGia = c.DonGia
                }).ToList()
            }).ToList();
        }

        public async Task<bool> PheduyetDonHangAsync(Guid donHangId)
        {
           var donhang= await _context.DonHangDBs.FirstOrDefaultAsync(dh => dh.MaDonHang == donHangId);
            if (donhang == null || donhang.TinhTrang != DonHangDB.TinhTrangDonhang.New)
            {
                return false; // Không tìm thấy đơn hàng hoặc trạng thái không hợp lệ
            }
            donhang.TinhTrang = DonHangDB.TinhTrangDonhang.Payment;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TaoDonHangAsync(TaoDonHangVM donHangVM)
        {
            var newDonhang = new DonHangDB
            {
                MaDonHang = Guid.NewGuid(),
                DiaChiGiao = donHangVM.DiaChiGiao,
                SoDienThoai = donHangVM.SoDienThoai,
                UserId = donHangVM.UserId,
                NgayDat = DateTime.Now,
                TinhTrang = DonHangDB.TinhTrangDonhang.New,
            };
            _context.DonHangDBs.Add(newDonhang);
            decimal tongTien = 0;
            foreach (var item in donHangVM.DanhSachHang)
            {
                var hanghoa = await _context.HangHoaDBs.FindAsync(item.MaHangHoa);
                if (hanghoa == null || item.SoLuong <= 0)
                {
                    continue; // Bỏ qua nếu hàng hóa không tồn tại hoặc số lượng không hợp lệ
                }
                var chiTiet = new ChiTietDonHangDB
                {
                    Id = Guid.NewGuid(),
                    MaDonHang = newDonhang.MaDonHang,
                    MaHangHoa = item.MaHangHoa,
                    SoLuong = item.SoLuong,
                    DonGia = hanghoa.DonGia,
                };

                tongTien+= hanghoa.DonGia * item.SoLuong;

                _context.ChiTietDonHangDBs.Add(chiTiet);
            }
            newDonhang.TongTien = tongTien;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
