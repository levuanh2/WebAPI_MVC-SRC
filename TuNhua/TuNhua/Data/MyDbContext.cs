using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TuNhua.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
           : base(options) { }

        
        public DbSet<UserDB> UserDBs { get; set; }
        public DbSet<HangHoaDB> HangHoaDBs { get; set; }
        public DbSet<DonHangDB> DonHangDBs { get; set; }
        public DbSet<ChiTietDonHangDB> ChiTietDonHangDBs { get; set; }
        public DbSet<LoaiHangHoaDB> LoaiHangHoaDBs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public object LoaiHangHoas { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 👤 User
            modelBuilder.Entity<UserDB>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .HasMaxLength(200);

                entity.HasOne(u => u.vaiTro)
                    .WithMany(v => v.UserDBs)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 👑 Vai trò
            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.ToTable("VaiTros");

                entity.HasKey(v => v.RoleId);

                entity.Property(v => v.TenVaiTro)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // 📦 Hàng hóa
            modelBuilder.Entity<HangHoaDB>(entity =>
            {
                entity.ToTable("HangHoas");

                entity.HasKey(h => h.MaHangHoa);

                entity.Property(h => h.TenHangHoa)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(h => h.DonGia)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(h => h.Loai)
                    .WithMany(l => l.HangHoas)
                    .HasForeignKey(h => h.LoaiId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 📂 Loại hàng hóa
            modelBuilder.Entity<LoaiHangHoaDB>(entity =>
            {
                entity.ToTable("LoaiHangHoas");

                entity.HasKey(l => l.LoaiId);

                entity.Property(l => l.TenLoai)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // 🧾 Đơn hàng
            modelBuilder.Entity<DonHangDB>(entity =>
            {
                entity.ToTable("DonHangs");

                entity.HasKey(d => d.MaDonHang);

                entity.Property(d => d.DiaChiGiao)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(d => d.SoDienThoai)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(d => d.TongTien)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.UserBD)
                    .WithMany(u => u.DonHangDBs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 📦 Chi tiết đơn hàng
            modelBuilder.Entity<ChiTietDonHangDB>(entity =>
            {
                entity.ToTable("ChiTietDonHangs");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.DonGia)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(c => c.DonHang)
                    .WithMany(d => d.ChiTietDonHangDBs)
                    .HasForeignKey(c => c.MaDonHang)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.HangHoa)
                    .WithMany(h => h.ChiTietDonHangs)
                    .HasForeignKey(c => c.MaHangHoa)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

