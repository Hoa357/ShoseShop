using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;

namespace ShoseShop.Data
{
    public class ShoesDbContext : DbContext
    {

        
        public ShoesDbContext() : base("name=ShoesDbContext")
        {
        }


        public virtual DbSet<BinhLuan> Binhluans { get; set; }

        public virtual DbSet<ChiTietPhieuMua> Chitietphieumuas { get; set; }

        public virtual DbSet<SanPham> Sanphams { get; set; }

        public virtual DbSet<KhachHang> Khachhangs { get; set; }

        public virtual DbSet<KhuyenMai> Khuyenmais { get; set; }

        public virtual DbSet<Loai> Loais { get; set; }

        public virtual DbSet<Mau> Maus { get; set; }

        public virtual DbSet<NhanVien> Nhanviens { get; set; }

        public virtual DbSet<PhieuMua> Phieumuas { get; set; }

        public virtual DbSet<Phuong> Phuongs { get; set; }

        public virtual DbSet<PhuongThucThanhToan> Phuongthucthanhtoans { get; set; }

        public virtual DbSet<Quan> Quans { get; set; }

      
        public virtual DbSet<SanPhamSize> Sanphamsizes { get; set; }

        public virtual DbSet<Size> Sizes { get; set; }

        public virtual DbSet<SoDiaChi> Sodiachis { get; set; }

        public virtual DbSet<TaiKhoan> Taikhoans { get; set; }

        public virtual DbSet<Tinh> Tinhs { get; set; }

        public virtual DbSet<Voucher> Vouchers { get; set; }

        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }

        public virtual DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            var binhluanConfig = modelBuilder.Entity<BinhLuan>();


            binhluanConfig.ToTable("BINHLUAN");
            binhluanConfig.HasKey(e => e.MaBinhLuan);


            binhluanConfig.Property(e => e.MaBinhLuan).HasColumnName("MABL");
            binhluanConfig.Property(e => e.MaSP).HasColumnName("MASP");
            binhluanConfig.Property(e => e.MaKH).HasColumnName("MAKH");
            binhluanConfig.Property(e => e.NgayBinhLuan)
                .HasColumnType("datetime") // HasColumnType vẫn giữ nguyên
                .HasColumnName("NGAYBL");
            binhluanConfig.Property(e => e.NoiDung).HasColumnName("NOIDUNGBL");
            binhluanConfig.Property(e => e.Rating).HasColumnName("RATING");

            // 4. Cấu hình mối quan hệ với DongSanPham
            binhluanConfig
                .HasRequired(d => d.MaSPNavigation)
                .WithMany(p => p.Binhluans)
                .HasForeignKey(d => d.MaSP)
                .WillCascadeOnDelete(false); // Dùng WillCascadeOnDelete(false) thay cho OnDelete

            // 5. Cấu hình mối quan hệ với KhachHang
            binhluanConfig
                .HasRequired(d => d.MaKHNavigation)
                .WithMany(p => p.Binhluans)
                .HasForeignKey(d => d.MaKH)
                .WillCascadeOnDelete(false);




            var chitetpmConfig = modelBuilder.Entity<ChiTietPhieuMua>();

            chitetpmConfig.HasKey(e => new { e.MaPhieuMua, e.MaSanPhamSize });

            chitetpmConfig.ToTable("CHITIETPHIEUMUA");

            chitetpmConfig.Property(e => e.MaPhieuMua).HasColumnName("MAPM");

            // 4. Cấu hình mối quan hệ với PhieuMua
            chitetpmConfig.HasRequired(d => d.MaPMNavigation).WithMany(p => p.ChiTietPhieuMuas)
                            .HasForeignKey(d => d.MaPhieuMua)
                            .WillCascadeOnDelete(false);

            // 4. Cấu hình mối quan hệ với SânPhamSize
            chitetpmConfig.HasRequired(d => d.MaSPSizeNavigation).WithMany(p => p.ChiTietPhieuMuas)
                    .HasForeignKey(d => d.MaSanPhamSize)
                     .WillCascadeOnDelete(false);



            var sanphamConfig = modelBuilder.Entity<SanPham>();


            sanphamConfig.HasKey(e => e.MaSanPham);

            sanphamConfig.ToTable("SANPHAM");


            sanphamConfig.Property(e => e.MaSanPham).HasColumnName("MASANPHAM");
            sanphamConfig.Property(e => e.GiaSanPham)
                    .HasColumnType("decimal")
                    .HasColumnName("GIAGOC");
            sanphamConfig.Property(e => e.Maloai).HasColumnName("MALOAI");
            sanphamConfig.Property(e => e.MoTa).HasColumnName("MOTA");
            sanphamConfig.Property(e => e.TenSanPham)
                    .HasMaxLength(255)
                    .HasColumnName("TENSP");

            // 4. Cấu hình mối quan hệ với Loai
            sanphamConfig.HasRequired(d => d.MaloaiNavigation).WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.Maloai)
                    .WillCascadeOnDelete(false);



            var khachangConfig = modelBuilder.Entity<KhachHang>();



            khachangConfig.HasKey(e => e.MaKhachHang);

            khachangConfig.ToTable("KHACHHANG");



            khachangConfig.Property(e => e.MaKhachHang).HasColumnName("MAKH");
            khachangConfig.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("EMAIL");
            khachangConfig.Property(e => e.GioiTinh).HasColumnName("GIOITINH");
            khachangConfig.Property(e => e.NgaySinh)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYSINH");
            khachangConfig.Property(e => e.SoĐT)
                    .HasMaxLength(255)
                    .HasColumnName("SDT");
            khachangConfig.Property(e => e.TenKhachHang)
                    .HasMaxLength(255)
                    .HasColumnName("TENKH");
            khachangConfig.Property(e => e.TongXu)
                    .HasColumnType("money")
                    .HasColumnName("TONGXU");

            // 4. Cấu hình mối quan hệ với TaiKhoan


            khachangConfig.HasRequired(kh => kh.EmailNavigation)
                .WithRequiredPrincipal(tk => tk.KhachHangs)
                .WillCascadeOnDelete(false);



            var khuyenmaiConfig = modelBuilder.Entity<KhuyenMai>();


            khuyenmaiConfig.HasKey(e => e.MaKhuyenMai);

            khuyenmaiConfig.ToTable("KHUYENMAI");

            khuyenmaiConfig.Property(e => e.MaKhuyenMai).HasColumnName("MAKM");
            khuyenmaiConfig.Property(e => e.NgayBatDau)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYBD");
            khuyenmaiConfig.Property(e => e.NgayKetThuc)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYKT");
            khuyenmaiConfig.Property(e => e.PhanTramGiam).HasColumnName("PHANTRAMGIAM");

            var chitietkhuyenmaiConfig = modelBuilder.Entity<ChiTietKhuyenMai>();

            // Đặt tên bảng
            chitietkhuyenmaiConfig.ToTable("CHITIETKHUYENMAI");

            // Khóa chính kép
            chitietkhuyenmaiConfig.HasKey(e => new { e.MaKM, e.MaSP });

            // Đặt tên cột
            chitietkhuyenmaiConfig.Property(e => e.MaKM).HasColumnName("MAKM");
            chitietkhuyenmaiConfig.Property(e => e.MaSP).HasColumnName("MASANPHAM");

            // Thiết lập mối quan hệ với bảng Khuyenmai
            chitietkhuyenmaiConfig.HasRequired(e => e.MaKMNavigation)
              .WithMany(k => k.ChiTietKhuyenMais)
              .HasForeignKey(e => e.MaKM)
              .WillCascadeOnDelete(false);


            // Thiết lập mối quan hệ với bảng Sanpham
            chitietkhuyenmaiConfig.HasRequired(e => e.MaSPNavigation)
                .WithMany(d => d.ChiTietKhuyenMais)
                .HasForeignKey(e => e.MaSP)
                .WillCascadeOnDelete(false);


            var loaiConfig = modelBuilder.Entity<Loai>();
  

            loaiConfig.HasKey(e => e.MaLoai);
            loaiConfig.ToTable("LOAI");

      

            loaiConfig.Property(e => e.MaLoai).HasColumnName("MALOAI");
            loaiConfig.Property(e => e.TenLoai)
                    .HasMaxLength(255)
                    .HasColumnName("TENLOAI");




            var mauConfig = modelBuilder.Entity<Mau>();

            mauConfig.HasKey(e => e.MaMau);

            mauConfig.ToTable("MAU");

            mauConfig.Property(e => e.MaMau)
                     .HasColumnName("MAMAU");
            mauConfig.Property(e => e.TenMau)
                    .HasMaxLength(255)
                    .HasColumnName("TENMAU");


            var nhanvienConfig = modelBuilder.Entity<NhanVien>();


            nhanvienConfig.HasKey(e => e.MaNV);
            nhanvienConfig.ToTable("NHANVIEN");


            nhanvienConfig.Property(e => e.MaNV).HasColumnName("MANV");
            nhanvienConfig.Property(e => e.DiaChi)
                    .HasMaxLength(255)
                    .HasColumnName("DIACHI");
            nhanvienConfig.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("EMAIL");
            nhanvienConfig.Property(e => e.GioiTinh).HasColumnName("GIOITINH");
            nhanvienConfig.Property(e => e.NgaySinh)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYSINH");
            nhanvienConfig.Property(e => e.SoDienThoai)
                    .HasMaxLength(255)
                    .HasColumnName("SDT");
            nhanvienConfig.Property(e => e.TenNV)
                    .HasMaxLength(255)
                    .HasColumnName("TENNV");


            // Thiết lập quan hệ 1-1 từ Nhanvien → TaiKhoan
            nhanvienConfig.HasRequired(nv => nv.EmailNavigation)
                .WithOptional(tk => tk.NhanViens)
                .Map(m => m.MapKey("EMAIL"));


            

            var phieumuasConfig = modelBuilder.Entity<PhieuMua>();

            phieumuasConfig.HasKey(e => e.MaPhieuMua);

            phieumuasConfig.ToTable("PHIEUMUA");

            phieumuasConfig.HasIndex(e => e.MaKH);





            phieumuasConfig.Property(e => e.MaPhieuMua).HasColumnName("MAPM");
            phieumuasConfig.Property(e => e.DiaChiNguoiNhan)
                    .HasMaxLength(255)
                    .HasColumnName("DIACHINGUOINHAN");
            phieumuasConfig.Property(e => e.EmailNguoiNhan)
                    .HasMaxLength(255)
                    .HasColumnName("EMAILNGUOINHAN");
            phieumuasConfig.Property(e => e.GhiChu).HasColumnName("GHICHU");
            phieumuasConfig.Property(e => e.LyDoHuyDon)
                    .HasMaxLength(255)
                    .HasColumnName("LYDOHUYDON");
            phieumuasConfig.Property(e => e.MaKH).HasColumnName("MAKH");
            phieumuasConfig.Property(e => e.MaNV).HasColumnName("MANV");
            phieumuasConfig.Property(e => e.MaPTTT).HasColumnName("MAPTTT");
            phieumuasConfig.Property(e => e.MaVoucher)
                    .HasMaxLength(255)
                    .HasColumnName("MAVOUCHER");
            phieumuasConfig.Property(e => e.NgayMua)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYDAT");
            phieumuasConfig.Property(e => e.SoDienThoaiNguoiNhan)
                    .HasMaxLength(255)
                    .HasColumnName("SDTNGUOINHAN");
            phieumuasConfig.Property(e => e.TenNguoiNhan)
                    .HasMaxLength(255)
                    .HasColumnName("TENNGUOINHAN");
            phieumuasConfig.Property(e => e.TinhTrang)
                    .HasMaxLength(255)
                    .HasColumnName("TINHTRANG");
            phieumuasConfig.Property(e => e.TongTien)
                    .HasColumnType("money")
                    .HasColumnName("TONGTIEN");



            phieumuasConfig.HasRequired(d => d.MaKHNavigation)
               .WithMany(p => p.Phieumuas)
               .HasForeignKey(d => d.MaKH);


            phieumuasConfig.HasRequired(d => d.MaNVNavigation).WithMany(p => p.PhieuMuas)
                    .HasForeignKey(d => d.MaNV);


            phieumuasConfig.HasRequired(d => d.MaPTTTNavigation).WithMany(p => p.PhieuMuas)
                    .HasForeignKey(d => d.MaPTTT)
                    .WillCascadeOnDelete(false);


            phieumuasConfig.HasRequired(d => d.MaVoucherNavigation).WithMany(p => p.Phieumuas)
                    .HasForeignKey(d => d.MaVoucher);



            var PhuongConfig = modelBuilder.Entity<Phuong>();


            PhuongConfig.HasKey(e => e.MaPhuong);

            PhuongConfig.ToTable("PHUONG");

            PhuongConfig.HasIndex(e => e.MaQuan);

            PhuongConfig.Property(e => e.MaPhuong)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                    .HasColumnName("MAPHUONG");
            PhuongConfig.Property(e => e.MaQuan).HasColumnName("MAQUAN");
            PhuongConfig.Property(e => e.TenPhuong)
                    .HasMaxLength(500)
                    .HasColumnName("TENPHUONG");

            PhuongConfig.HasRequired(d => d.MaQuanNavigation).WithMany(p => p.Phuongs)
                    .HasForeignKey(d => d.MaQuan);



            var phuongthucthanhtoanConfig = modelBuilder.Entity<PhuongThucThanhToan>();


            phuongthucthanhtoanConfig.HasKey(e => e.MaPTTT);

            phuongthucthanhtoanConfig.ToTable("PHUONGTHUCTHANHTOAN");

            phuongthucthanhtoanConfig.Property(e => e.MaPTTT).HasColumnName("MAPTTT");
            phuongthucthanhtoanConfig.Property(e => e.TenPTTT)
                    .HasMaxLength(255)
                    .HasColumnName("TENPHUONGTHUC");


            var QuanConfig = modelBuilder.Entity<Quan>();
            QuanConfig.HasKey(e => e.MaQuan);

            QuanConfig.ToTable("QUAN");

            QuanConfig.Property(e => e.MaQuan)
                    .HasColumnName("MAQUAN");
            QuanConfig.Property(e => e.MaTinh).HasColumnName("MATINH");
            QuanConfig.Property(e => e.TenQuan)
                    .HasMaxLength(500)
                    .HasColumnName("TENQUAN");

            QuanConfig.HasRequired(d => d.MaTinhNavigation).WithMany(p => p.Quans)
                    .HasForeignKey(d => d.MaTinh);


            var ChiTietSanPhamConfig = modelBuilder.Entity<ChiTietSanPham>();

            ChiTietSanPhamConfig.HasKey(e => e.MaChiTietSP);

            ChiTietSanPhamConfig.ToTable("CHITIETSANPHAM");

          

            ChiTietSanPhamConfig.Property(e => e.MaChiTietSP).HasColumnName("MASPCT");
            ChiTietSanPhamConfig.Property(e => e.AnhDaiDien).HasColumnName("ANHDAIDIEN");
            ChiTietSanPhamConfig.Property(e => e.AnhDeGiay).HasColumnName("ANHDEGIAY");
            ChiTietSanPhamConfig.Property(e => e.AnhMatTren).HasColumnName("ANHMATTREN");
            ChiTietSanPhamConfig.Property(e => e.MaSP).HasColumnName("MASANPHAM");
            ChiTietSanPhamConfig.Property(e => e.MaMau)
                    .HasColumnName("MAMAU");
            ChiTietSanPhamConfig.Property(e => e.TrangThai).HasColumnName("TRANGTHAI");
            ChiTietSanPhamConfig.Property(e => e.Video).HasColumnName("VIDEO");

            ChiTietSanPhamConfig.HasRequired(d => d.MaSPNavigation).WithMany(p => p.ChiTietSanPhams)
                    .HasForeignKey(d => d.MaSP);


            ChiTietSanPhamConfig.HasRequired(d => d.MaMauNavigation).WithMany(p => p.ChiTietSanPhams)
                    .HasForeignKey(d => d.MaMau);


            var sanphamSizeConfig = modelBuilder.Entity<SanPhamSize>();

            sanphamSizeConfig.HasKey(e => e.MaSanPhamSize);

            sanphamSizeConfig.ToTable("SANPHAMSIZE");

            

            sanphamSizeConfig.Property(e => e.MaSanPhamSize).HasColumnName("MASPSIZE");
            sanphamSizeConfig.Property(e => e.Masize).HasColumnName("MASIZE");
            sanphamSizeConfig.Property(e => e.Maspct).HasColumnName("MASPCT");
            sanphamSizeConfig.Property(e => e.SoLuongTonKho).HasColumnName("SLTON");

            sanphamSizeConfig.HasRequired(d => d.MaSizeNavigation).WithMany(p => p.SanPhamSizes)
                    .HasForeignKey(d => d.Masize);



            sanphamSizeConfig.HasRequired(d => d.MaSPCTNavigation).WithMany(p => p.Sanphamsizes)
                .HasForeignKey(d => d.Maspct);


            var Sizeconfiguration = modelBuilder.Entity<Size>();

            Sizeconfiguration.HasKey(e => e.MaSize);

            Sizeconfiguration.ToTable("SIZE");

            Sizeconfiguration.Property(e => e.MaSize).HasColumnName("MASIZE");
            Sizeconfiguration.Property(e => e.TenSize)
                    .HasMaxLength(255)
                    .HasColumnName("TENSIZE");


            var SoDiaChiconfiguration = modelBuilder.Entity<SoDiaChi>();


            SoDiaChiconfiguration.HasKey(e => e.MaSoDiaChi);

            SoDiaChiconfiguration.ToTable("SODIACHI");

            SoDiaChiconfiguration.HasIndex(e => e.MaKH);

            SoDiaChiconfiguration.Property(e => e.MaSoDiaChi).HasColumnName("MASODIACHI");
            SoDiaChiconfiguration.Property(e => e.DiaChi)
                    .HasMaxLength(500)
                    .HasColumnName("DIACHI");
            SoDiaChiconfiguration.Property(e => e.MaKH).HasColumnName("MAKH");
            SoDiaChiconfiguration.Property(e => e.SoDTNguoiNhan)
                    .HasMaxLength(255)
                    .HasColumnName("SDTNGUOINHAN");
            SoDiaChiconfiguration.Property(e => e.TenNguoiNhan)
                    .HasMaxLength(500)
                    .HasColumnName("TENNGUOINHAN");

            SoDiaChiconfiguration.HasRequired(d => d.MaKHNavigation).WithMany(p => p.Sodiachis)
                    .HasForeignKey(d => d.MaKH);

            var TaiKhoanconfiguration = modelBuilder.Entity<TaiKhoan>();



            TaiKhoanconfiguration.HasKey(e => e.Email);

            TaiKhoanconfiguration.ToTable("TAIKHOAN");

            TaiKhoanconfiguration.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("EMAIL");
            TaiKhoanconfiguration.Property(e => e.LoaiTK).HasColumnName("LOAITK");
            TaiKhoanconfiguration.Property(e => e.MatKhau)
                    .HasMaxLength(255)
                    .HasColumnName("MATKHAU");


            var Tinhconfiguration = modelBuilder.Entity<Tinh>();


            Tinhconfiguration.HasKey(e => e.Matinh);

            Tinhconfiguration.ToTable("TINH");

            Tinhconfiguration.Property(e => e.Matinh)
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                    .HasColumnName("MATINH");
            Tinhconfiguration.Property(e => e.Tentinh)
                    .HasMaxLength(500)
                    .HasColumnName("TENTINH");

            var Voucherconfiguration = modelBuilder.Entity<Voucher>();


            Voucherconfiguration.HasKey(e => e.MaVoucher);

            Voucherconfiguration.ToTable("VOUCHER");

            Voucherconfiguration.Property(e => e.MaVoucher)
                    .HasMaxLength(255)
                    .HasColumnName("MAVOUCHER");
            Voucherconfiguration.Property(e => e.GiaToiDa)
                    .HasColumnType("money")
                    .HasColumnName("GIAMTOIDA");
            Voucherconfiguration.Property(e => e.GiaToiThieu)
                    .HasColumnType("money")
                    .HasColumnName("GIATOITHIEU");
            Voucherconfiguration.Property(e => e.NgayKetThuc)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYHETHAN");
            Voucherconfiguration.Property(e => e.NgayBatDau)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYTAO");
            Voucherconfiguration.Property(e => e.SoLuong).HasColumnName("SOLUONG");



        }


    }
}


