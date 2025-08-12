namespace ShoseShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banner",
                c => new
                    {
                        MABANNER = c.Int(nullable: false, identity: true),
                        TENBANNER = c.String(maxLength: 255),
                        VITRI = c.String(),
                        LINK = c.String(),
                        HOATDONG = c.Boolean(nullable: false),
                        SLOGAN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MABANNER);
            
            CreateTable(
                "dbo.BinhLuan",
                c => new
                    {
                        MABL = c.Int(nullable: false, identity: true),
                        NOIDUNGBL = c.String(),
                        MASP = c.Int(nullable: false),
                        MAKH = c.Int(nullable: false),
                        RATING = c.Int(nullable: false),
                        NGAYBL = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MABL)
                .ForeignKey("dbo.KHACHHANG", t => t.MAKH)
                .ForeignKey("dbo.SANPHAM", t => t.MASP)
                .Index(t => t.MASP)
                .Index(t => t.MAKH);
            
            CreateTable(
                "dbo.KHACHHANG",
                c => new
                    {
                        MAKH = c.Int(nullable: false, identity: true),
                        TENKH = c.String(maxLength: 255),
                        EMAIL = c.String(maxLength: 255),
                        SDT = c.String(maxLength: 255),
                        GIOITINH = c.Boolean(),
                        NGAYSINH = c.DateTime(nullable: false),
                        TONGXU = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.MAKH);
            
            CreateTable(
                "dbo.TAIKHOAN",
                c => new
                    {
                        EMAIL = c.String(nullable: false, maxLength: 255),
                        LOAITK = c.Int(nullable: false),
                        MATKHAU = c.String(maxLength: 255),
                        NhanViens_MaNV = c.Int(nullable: false),
                        KhachHangs_MaKhachHang = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EMAIL)
                .ForeignKey("dbo.NHANVIEN", t => t.NhanViens_MaNV)
                .ForeignKey("dbo.KHACHHANG", t => t.KhachHangs_MaKhachHang)
                .Index(t => t.NhanViens_MaNV)
                .Index(t => t.KhachHangs_MaKhachHang);
            
            CreateTable(
                "dbo.NHANVIEN",
                c => new
                    {
                        MANV = c.Int(nullable: false, identity: true),
                        TENNV = c.String(maxLength: 255),
                        EMAILL = c.String(maxLength: 255),
                        SDT = c.String(maxLength: 255),
                        NGAYSINH = c.DateTime(nullable: false),
                        GIOITINH = c.Boolean(nullable: false),
                        DIACHI = c.String(maxLength: 255),
                        ChucVu = c.String(),
                        NgayVaoLam = c.DateTime(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                        MaPMNavigation_MaPhieuMua = c.Int(),
                    })
                .PrimaryKey(t => t.MANV)
                .ForeignKey("dbo.PHIEUMUA", t => t.MaPMNavigation_MaPhieuMua)
                .Index(t => t.MaPMNavigation_MaPhieuMua);
            
            CreateTable(
                "dbo.PHIEUMUA",
                c => new
                    {
                        MAPM = c.Int(nullable: false, identity: true),
                        NGAYDAT = c.DateTime(nullable: false),
                        MAKH = c.Int(nullable: false),
                        MANV = c.Int(nullable: false),
                        MAVOUCHER = c.String(nullable: false, maxLength: 255),
                        MAPTTT = c.Int(nullable: false),
                        TINHTRANG = c.String(maxLength: 255),
                        GHICHU = c.String(),
                        LYDOHUYDON = c.String(maxLength: 255),
                        TONGTIEN = c.Decimal(nullable: false, storeType: "money"),
                        NgayHuyDon = c.DateTime(),
                        DIACHINGUOINHAN = c.String(maxLength: 255),
                        EMAILNGUOINHAN = c.String(maxLength: 255),
                        SDTNGUOINHAN = c.String(maxLength: 255),
                        TENNGUOINHAN = c.String(maxLength: 255),
                        MaPMNavigation_MaPhieuMua = c.Int(),
                    })
                .PrimaryKey(t => t.MAPM)
                .ForeignKey("dbo.KHACHHANG", t => t.MAKH, cascadeDelete: true)
                .ForeignKey("dbo.NHANVIEN", t => t.MANV, cascadeDelete: true)
                .ForeignKey("dbo.PHIEUMUA", t => t.MaPMNavigation_MaPhieuMua)
                .ForeignKey("dbo.PHUONGTHUCTHANHTOAN", t => t.MAPTTT)
                .ForeignKey("dbo.VOUCHER", t => t.MAVOUCHER, cascadeDelete: true)
                .Index(t => t.MAKH)
                .Index(t => t.MANV)
                .Index(t => t.MAVOUCHER)
                .Index(t => t.MAPTTT)
                .Index(t => t.MaPMNavigation_MaPhieuMua);
            
            CreateTable(
                "dbo.CHITIETPHIEUMUA",
                c => new
                    {
                        MAPM = c.Int(nullable: false),
                        MaSanPhamSize = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.MAPM, t.MaSanPhamSize })
                .ForeignKey("dbo.PHIEUMUA", t => t.MAPM)
                .ForeignKey("dbo.SANPHAMSIZE", t => t.MaSanPhamSize)
                .Index(t => t.MAPM)
                .Index(t => t.MaSanPhamSize);
            
            CreateTable(
                "dbo.SANPHAMSIZE",
                c => new
                    {
                        MASPSIZE = c.Int(nullable: false, identity: true),
                        MASPCT = c.Int(nullable: false),
                        MASIZE = c.Int(nullable: false),
                        SLTON = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MASPSIZE)
                .ForeignKey("dbo.SIZE", t => t.MASIZE, cascadeDelete: true)
                .ForeignKey("dbo.CHITIETSANPHAM", t => t.MASPCT, cascadeDelete: true)
                .Index(t => t.MASPCT)
                .Index(t => t.MASIZE);
            
            CreateTable(
                "dbo.SIZE",
                c => new
                    {
                        MASIZE = c.Int(nullable: false, identity: true),
                        TENSIZE = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MASIZE);
            
            CreateTable(
                "dbo.CHITIETSANPHAM",
                c => new
                    {
                        MASPCT = c.Int(nullable: false, identity: true),
                        MASANPHAM = c.Int(nullable: false),
                        MAMAU = c.Int(nullable: false),
                        ANHDAIDIEN = c.String(),
                        ANHMATTREN = c.String(),
                        ANHDEGIAY = c.String(),
                        VIDEO = c.String(),
                        TRANGTHAI = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MASPCT)
                .ForeignKey("dbo.MAU", t => t.MAMAU, cascadeDelete: true)
                .ForeignKey("dbo.SANPHAM", t => t.MASANPHAM, cascadeDelete: true)
                .Index(t => t.MASANPHAM)
                .Index(t => t.MAMAU);
            
            CreateTable(
                "dbo.MAU",
                c => new
                    {
                        MAMAU = c.Int(nullable: false, identity: true),
                        TENMAU = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MAMAU);
            
            CreateTable(
                "dbo.SANPHAM",
                c => new
                    {
                        MASANPHAM = c.Int(nullable: false, identity: true),
                        TENSP = c.String(maxLength: 255),
                        MALOAI = c.Int(nullable: false),
                        GIAGOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MOTA = c.String(),
                    })
                .PrimaryKey(t => t.MASANPHAM)
                .ForeignKey("dbo.LOAI", t => t.MALOAI)
                .Index(t => t.MALOAI);
            
            CreateTable(
                "dbo.CHITIETKHUYENMAI",
                c => new
                    {
                        MAKM = c.Int(nullable: false),
                        MASANPHAM = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MAKM, t.MASANPHAM })
                .ForeignKey("dbo.KHUYENMAI", t => t.MAKM)
                .ForeignKey("dbo.SANPHAM", t => t.MASANPHAM)
                .Index(t => t.MAKM)
                .Index(t => t.MASANPHAM);
            
            CreateTable(
                "dbo.KHUYENMAI",
                c => new
                    {
                        MAKM = c.Int(nullable: false, identity: true),
                        TenKhuyenMai = c.String(),
                        NGAYBD = c.DateTime(nullable: false),
                        NGAYKT = c.DateTime(nullable: false),
                        PHANTRAMGIAM = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MAKM);
            
            CreateTable(
                "dbo.LOAI",
                c => new
                    {
                        MALOAI = c.Int(nullable: false, identity: true),
                        TENLOAI = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MALOAI);
            
            CreateTable(
                "dbo.PHUONGTHUCTHANHTOAN",
                c => new
                    {
                        MAPTTT = c.Int(nullable: false, identity: true),
                        TENPHUONGTHUC = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.MAPTTT);
            
            CreateTable(
                "dbo.VOUCHER",
                c => new
                    {
                        MAVOUCHER = c.String(nullable: false, maxLength: 255),
                        SOLUONG = c.Int(nullable: false),
                        GIATOITHIEU = c.Decimal(nullable: false, storeType: "money"),
                        GIAMTOIDA = c.Decimal(nullable: false, storeType: "money"),
                        NGAYTAO = c.DateTime(nullable: false),
                        NGAYHETHAN = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MAVOUCHER);
            
            CreateTable(
                "dbo.SoDiaChi",
                c => new
                    {
                        MASoDiaChi = c.Int(nullable: false, identity: true),
                        MAKH = c.Int(nullable: false),
                        TENNGUOINHAN = c.String(maxLength: 500),
                        SDTNGUOINHAN = c.String(maxLength: 255),
                        DIACHI = c.String(maxLength: 500),
                        KhachHang_MaKhachHang = c.Int(),
                    })
                .PrimaryKey(t => t.MASoDiaChi)
                .ForeignKey("dbo.KHACHHANG", t => t.MAKH, cascadeDelete: true)
                .ForeignKey("dbo.KHACHHANG", t => t.KhachHang_MaKhachHang)
                .Index(t => t.MAKH)
                .Index(t => t.KhachHang_MaKhachHang);
            
            CreateTable(
                "dbo.PHUONG",
                c => new
                    {
                        MAPHUONG = c.Int(nullable: false),
                        TENPHUONG = c.String(maxLength: 500),
                        MAQUAN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MAPHUONG)
                .ForeignKey("dbo.QUAN", t => t.MAQUAN, cascadeDelete: true)
                .Index(t => t.MAQUAN);
            
            CreateTable(
                "dbo.QUAN",
                c => new
                    {
                        MAQUAN = c.Int(nullable: false, identity: true),
                        TENQUAN = c.String(maxLength: 500),
                        MATINH = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MAQUAN)
                .ForeignKey("dbo.TINH", t => t.MATINH, cascadeDelete: true)
                .Index(t => t.MATINH);
            
            CreateTable(
                "dbo.TINH",
                c => new
                    {
                        MATINH = c.Int(nullable: false),
                        TENTINH = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.MATINH);
            
            CreateTable(
                "dbo.KhuyenMaiSanPhams",
                c => new
                    {
                        KhuyenMai_MaKhuyenMai = c.Int(nullable: false),
                        SanPham_MaSanPham = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.KhuyenMai_MaKhuyenMai, t.SanPham_MaSanPham })
                .ForeignKey("dbo.KHUYENMAI", t => t.KhuyenMai_MaKhuyenMai, cascadeDelete: true)
                .ForeignKey("dbo.SANPHAM", t => t.SanPham_MaSanPham, cascadeDelete: true)
                .Index(t => t.KhuyenMai_MaKhuyenMai)
                .Index(t => t.SanPham_MaSanPham);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PHUONG", "MAQUAN", "dbo.QUAN");
            DropForeignKey("dbo.QUAN", "MATINH", "dbo.TINH");
            DropForeignKey("dbo.BinhLuan", "MASP", "dbo.SANPHAM");
            DropForeignKey("dbo.BinhLuan", "MAKH", "dbo.KHACHHANG");
            DropForeignKey("dbo.SoDiaChi", "KhachHang_MaKhachHang", "dbo.KHACHHANG");
            DropForeignKey("dbo.SoDiaChi", "MAKH", "dbo.KHACHHANG");
            DropForeignKey("dbo.TAIKHOAN", "KhachHangs_MaKhachHang", "dbo.KHACHHANG");
            DropForeignKey("dbo.TAIKHOAN", "NhanViens_MaNV", "dbo.NHANVIEN");
            DropForeignKey("dbo.NHANVIEN", "MaPMNavigation_MaPhieuMua", "dbo.PHIEUMUA");
            DropForeignKey("dbo.PHIEUMUA", "MAVOUCHER", "dbo.VOUCHER");
            DropForeignKey("dbo.PHIEUMUA", "MAPTTT", "dbo.PHUONGTHUCTHANHTOAN");
            DropForeignKey("dbo.PHIEUMUA", "MaPMNavigation_MaPhieuMua", "dbo.PHIEUMUA");
            DropForeignKey("dbo.PHIEUMUA", "MANV", "dbo.NHANVIEN");
            DropForeignKey("dbo.PHIEUMUA", "MAKH", "dbo.KHACHHANG");
            DropForeignKey("dbo.CHITIETPHIEUMUA", "MaSanPhamSize", "dbo.SANPHAMSIZE");
            DropForeignKey("dbo.SANPHAMSIZE", "MASPCT", "dbo.CHITIETSANPHAM");
            DropForeignKey("dbo.CHITIETSANPHAM", "MASANPHAM", "dbo.SANPHAM");
            DropForeignKey("dbo.SANPHAM", "MALOAI", "dbo.LOAI");
            DropForeignKey("dbo.CHITIETKHUYENMAI", "MASANPHAM", "dbo.SANPHAM");
            DropForeignKey("dbo.CHITIETKHUYENMAI", "MAKM", "dbo.KHUYENMAI");
            DropForeignKey("dbo.KhuyenMaiSanPhams", "SanPham_MaSanPham", "dbo.SANPHAM");
            DropForeignKey("dbo.KhuyenMaiSanPhams", "KhuyenMai_MaKhuyenMai", "dbo.KHUYENMAI");
            DropForeignKey("dbo.CHITIETSANPHAM", "MAMAU", "dbo.MAU");
            DropForeignKey("dbo.SANPHAMSIZE", "MASIZE", "dbo.SIZE");
            DropForeignKey("dbo.CHITIETPHIEUMUA", "MAPM", "dbo.PHIEUMUA");
            DropIndex("dbo.KhuyenMaiSanPhams", new[] { "SanPham_MaSanPham" });
            DropIndex("dbo.KhuyenMaiSanPhams", new[] { "KhuyenMai_MaKhuyenMai" });
            DropIndex("dbo.QUAN", new[] { "MATINH" });
            DropIndex("dbo.PHUONG", new[] { "MAQUAN" });
            DropIndex("dbo.SoDiaChi", new[] { "KhachHang_MaKhachHang" });
            DropIndex("dbo.SoDiaChi", new[] { "MAKH" });
            DropIndex("dbo.CHITIETKHUYENMAI", new[] { "MASANPHAM" });
            DropIndex("dbo.CHITIETKHUYENMAI", new[] { "MAKM" });
            DropIndex("dbo.SANPHAM", new[] { "MALOAI" });
            DropIndex("dbo.CHITIETSANPHAM", new[] { "MAMAU" });
            DropIndex("dbo.CHITIETSANPHAM", new[] { "MASANPHAM" });
            DropIndex("dbo.SANPHAMSIZE", new[] { "MASIZE" });
            DropIndex("dbo.SANPHAMSIZE", new[] { "MASPCT" });
            DropIndex("dbo.CHITIETPHIEUMUA", new[] { "MaSanPhamSize" });
            DropIndex("dbo.CHITIETPHIEUMUA", new[] { "MAPM" });
            DropIndex("dbo.PHIEUMUA", new[] { "MaPMNavigation_MaPhieuMua" });
            DropIndex("dbo.PHIEUMUA", new[] { "MAPTTT" });
            DropIndex("dbo.PHIEUMUA", new[] { "MAVOUCHER" });
            DropIndex("dbo.PHIEUMUA", new[] { "MANV" });
            DropIndex("dbo.PHIEUMUA", new[] { "MAKH" });
            DropIndex("dbo.NHANVIEN", new[] { "MaPMNavigation_MaPhieuMua" });
            DropIndex("dbo.TAIKHOAN", new[] { "KhachHangs_MaKhachHang" });
            DropIndex("dbo.TAIKHOAN", new[] { "NhanViens_MaNV" });
            DropIndex("dbo.BinhLuan", new[] { "MAKH" });
            DropIndex("dbo.BinhLuan", new[] { "MASP" });
            DropTable("dbo.KhuyenMaiSanPhams");
            DropTable("dbo.TINH");
            DropTable("dbo.QUAN");
            DropTable("dbo.PHUONG");
            DropTable("dbo.SoDiaChi");
            DropTable("dbo.VOUCHER");
            DropTable("dbo.PHUONGTHUCTHANHTOAN");
            DropTable("dbo.LOAI");
            DropTable("dbo.KHUYENMAI");
            DropTable("dbo.CHITIETKHUYENMAI");
            DropTable("dbo.SANPHAM");
            DropTable("dbo.MAU");
            DropTable("dbo.CHITIETSANPHAM");
            DropTable("dbo.SIZE");
            DropTable("dbo.SANPHAMSIZE");
            DropTable("dbo.CHITIETPHIEUMUA");
            DropTable("dbo.PHIEUMUA");
            DropTable("dbo.NHANVIEN");
            DropTable("dbo.TAIKHOAN");
            DropTable("dbo.KHACHHANG");
            DropTable("dbo.BinhLuan");
            DropTable("dbo.Banner");
        }
    }
}
