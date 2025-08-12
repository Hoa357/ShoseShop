namespace ShoseShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShoseShop.Data.ShoesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShoseShop.Data.ShoesContext _db)
        {
           
                _db.Banner.AddOrUpdate(
                    b => b.Mabanner,
                    new Data.Banner { Mabanner = 1, Tenbanner = "Rurring New Shoes", Vitri = "Top", Link = "265958f0-b850-4569-a892-86a316d6d912_banner-img.png", Hoatdong = true, Slogan = "RUN FASTER" },
                    new Data.Banner { Mabanner = 2, Tenbanner = "Football Shoes New", Vitri = "Bottom", Link = "ec8fe1bc-19c3-49ac-9b89-64bff87e5f2f_football-shoes-amazon-scaled-692x376.jpg", Hoatdong = true, Slogan = "SCORE THE GOAL" }
                );

                _db.Phuongthucthanhtoans.AddOrUpdate(
                    p => p.MaPTTT,
                    new Data.PhuongThucThanhToan { MaPTTT = 1, TenPTTT = "Thanh toán khi nhận hàng" },
                    new Data.PhuongThucThanhToan { MaPTTT = 2, TenPTTT = "Thanh Toán VNPay" }

               );

                _db.Sizes.AddOrUpdate(
                    s => s.MaSize,

                    new Data.Size { MaSize = 1, TenSize = "38" },
                    new Data.Size { MaSize = 2, TenSize = "39" },
                    new Data.Size { MaSize = 3, TenSize = "40" },
                    new Data.Size { MaSize = 4, TenSize = "41" }

                );

                _db.Maus.AddOrUpdate(
                    m => m.MaMau,
                    new Data.Mau { MaMau = 1, TenMau = "Đen" },
                    new Data.Mau { MaMau = 2, TenMau = "Trắng" },
                    new Data.Mau { MaMau = 3, TenMau = "Xanh" },
                    new Data.Mau { MaMau = 4, TenMau = "Đỏ" },
                     new Data.Mau { MaMau = 5, TenMau = "Hồng" },
                    new Data.Mau { MaMau = 6, TenMau = "Xám" },
                    new Data.Mau { MaMau = 7, TenMau = "Nâu" },
                     new Data.Mau { MaMau = 8, TenMau = "Vàng" }

                );

                _db.Loais.AddOrUpdate(
                    l => l.MaLoai,
                    new Data.Loai { MaLoai = 1, TenLoai = "Bóng đá" },
                    new Data.Loai { MaLoai = 2, TenLoai = "Bóng rổ" },
                    new Data.Loai { MaLoai = 3, TenLoai = "Chạy bộ" }


                    );

                _db.Sanphams.AddOrUpdate(
                    s => s.MaSanPham,
                    new Data.SanPham { MaSanPham = 1, Maloai = 1, TenSanPham = "Copa Pure 2 Elite FG", GiaSanPham = 500000, MoTa = "Giày đá bóng da thật cao cấp với công nghệ Fusionskin liền mạch, ôm chân êm ái, cho cảm giác bóng tối ưu và bám sân chắc chắn trên cỏ tự nhiên" },
                    new Data.SanPham { MaSanPham = 2, Maloai = 3, TenSanPham = "Duramo SL 2.0", GiaSanPham = 1900000, MoTa = "Giày chạy bộ nhẹ, êm ái với đệm LIGHTMOTION và lưới thoáng khí, mang lại cảm giác thoải mái và hỗ trợ tối ưu cho luyện tập hằng ngày." },
                    new Data.SanPham { MaSanPham = 3, Maloai = 3, TenSanPham = "Supernova Rise", GiaSanPham = 4000000, MoTa = "Giày chạy bộ hiệu suất cao với đệm Dreamstrike+ êm ái, thiết kế ôm chân và đế ngoài bám tốt, mang lại trải nghiệm êm mượt trên mọi quãng đường." },
                    new Data.SanPham { MaSanPham = 4, Maloai = 1, TenSanPham = "Crazyfast Elite", GiaSanPham = 2900000, MoTa = "Giày đá bóng Adidas X Crazyfast Elite FG – thiết kế tối giản siêu nhẹ, upper Aeropacity Speedskin trong suốt, đế Lightstrike cho tăng tốc nhanh chóng, bám sân chắc trên cỏ tự nhiên" },
                    new Data.SanPham { MaSanPham = 5, Maloai = 3, TenSanPham = "Rivarly Low", GiaSanPham = 2100000, MoTa = "Sneaker cổ thấp phong cách retro, chất liệu da mềm kết hợp đế cao su bền chắc, mang lại sự thoải mái và dễ phối đồ cho phong cách hằng ngày." },
                    new Data.SanPham { MaSanPham = 6, Maloai = 2, TenSanPham = "Nike Air Zoom GT Cut 3", GiaSanPham = 3000000, MoTa = "Giày bóng rổ nhẹ, đệm Zoom Air phản hồi nhanh, bám sân tốt, hỗ trợ di chuyển linh hoạt và tăng tốc." },
                    new Data.SanPham { MaSanPham = 7, Maloai = 2, TenSanPham = "Adidas Harden Vol. 8", GiaSanPham = 2000000, MoTa = "hiết kế hiện đại lấy cảm hứng từ James Harden, đệm Lightstrike êm ái, hỗ trợ bứt tốc và đổi hướng mượt mà." }

                );



                _db.ChiTietSanPhams.AddOrUpdate(
                    s => s.MaChiTietSP,

                    new Data.ChiTietSanPham { MaChiTietSP = 1, MaSP = 1, MaMau = 4, AnhDaiDien = "do1.jpg", AnhMatTren = "do2.jpg", AnhDeGiay = "do3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.DangBan },
                     new Data.ChiTietSanPham { MaChiTietSP = 2, MaSP = 1, MaMau = 2, AnhDaiDien = "trang1.jpg", AnhMatTren = "trang2.jpg", AnhDeGiay = "trang3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.New },
                      new Data.ChiTietSanPham { MaChiTietSP = 3, MaSP = 2, MaMau = 2, AnhDaiDien = "trang1.jpg", AnhMatTren = "trang2.jpg", AnhDeGiay = "trang3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.DangBan },
                       new Data.ChiTietSanPham { MaChiTietSP = 4, MaSP = 2, MaMau = 4, AnhDaiDien = "do1.jpg", AnhMatTren = "do2.jpg", AnhDeGiay = "do3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.New },


                       new Data.ChiTietSanPham { MaChiTietSP = 5, MaSP = 3, MaMau = 3, AnhDaiDien = "xanh1.jpg", AnhMatTren = "xanh2.jpg", AnhDeGiay = "xanh3.jpg", Video = "xanh.webm", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.Hot },
                       new Data.ChiTietSanPham { MaChiTietSP = 6, MaSP = 3, MaMau = 4, AnhDaiDien = "do1.jpg", AnhMatTren = "do2.jpg", AnhDeGiay = "do3.jpg", Video = "do.webm", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.Hot },
                       new Data.ChiTietSanPham { MaChiTietSP = 7, MaSP = 3, MaMau = 1, AnhDaiDien = "den1.jpg", AnhMatTren = "den2.jpg", AnhDeGiay = "den3.jpg", Video = "den.webm", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.Hot },


                       new Data.ChiTietSanPham { MaChiTietSP = 8, MaSP = 4, MaMau = 8, AnhDaiDien = "vang1.jpg", AnhMatTren = "vang2.jpg", AnhDeGiay = "vang3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.New },
                       new Data.ChiTietSanPham { MaChiTietSP = 9, MaSP = 4, MaMau = 1, AnhDaiDien = "den1.jpg", AnhMatTren = "den2.jpg", AnhDeGiay = "den3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.New },



                       new Data.ChiTietSanPham { MaChiTietSP = 10, MaSP = 5, MaMau = 2, AnhDaiDien = "trang1.jpg", AnhMatTren = "trang2.jpg", AnhDeGiay = "trang3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.Hot },
                       new Data.ChiTietSanPham { MaChiTietSP = 11, MaSP = 5, MaMau = 1, AnhDaiDien = "den1.jpg", AnhMatTren = "den2.jpg", AnhDeGiay = "den3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.Hot },


                       new Data.ChiTietSanPham { MaChiTietSP = 12, MaSP = 6, MaMau = 2, AnhDaiDien = "trang1.jpg", AnhMatTren = "trang2.jpg", AnhDeGiay = "trang3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.DangBan },
                       new Data.ChiTietSanPham { MaChiTietSP = 13, MaSP = 6, MaMau = 1, AnhDaiDien = "den1.jpg", AnhMatTren = "den2.jpg", AnhDeGiay = "den3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.New },



                       new Data.ChiTietSanPham { MaChiTietSP = 14, MaSP = 7, MaMau = 2, AnhDaiDien = "trang1.jpg", AnhMatTren = "trang2.jpg", AnhDeGiay = "trang3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.New },
                       new Data.ChiTietSanPham { MaChiTietSP = 15, MaSP = 7, MaMau = 1, AnhDaiDien = "den1.jpg", AnhMatTren = "den2.jpg", AnhDeGiay = "den3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.New },
                       new Data.ChiTietSanPham { MaChiTietSP = 16, MaSP = 7, MaMau = 3, AnhDaiDien = "xanh1.jpg", AnhMatTren = "xanh2.jpg", AnhDeGiay = "xanh3.jpg", Video = "", TrangThai = Data.ChiTietSanPham.TrangThaiEnum.DangBan }

                    );


                _db.Sanphamsizes.AddOrUpdate(
                    s => s.MaSanPhamSize,



                    new Data.SanPhamSize { MaSanPhamSize = 1, Maspct = 1, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 2, Maspct = 1, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 3, Maspct = 1, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 4, Maspct = 1, Masize = 4, SoLuongTonKho = 5 },



                     new Data.SanPhamSize { MaSanPhamSize = 5, Maspct = 2, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 6, Maspct = 2, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 7, Maspct = 2, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 8, Maspct = 2, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 9, Maspct = 3, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 10, Maspct = 3, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 11, Maspct = 3, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 12, Maspct = 3, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 13, Maspct = 4, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 14, Maspct = 4, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 15, Maspct = 4, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 16, Maspct = 4, Masize = 4, SoLuongTonKho = 5 },



                     new Data.SanPhamSize { MaSanPhamSize = 17, Maspct = 5, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 18, Maspct = 5, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 19, Maspct = 5, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 20, Maspct = 5, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 21, Maspct = 6, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 22, Maspct = 6, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 23, Maspct = 6, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 24, Maspct = 6, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 25, Maspct = 7, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 26, Maspct = 7, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 27, Maspct = 7, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 28, Maspct = 7, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 29, Maspct = 8, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 30, Maspct = 8, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 31, Maspct = 8, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 32, Maspct = 8, Masize = 4, SoLuongTonKho = 5 },



                     new Data.SanPhamSize { MaSanPhamSize = 33, Maspct = 9, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 34, Maspct = 9, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 35, Maspct = 9, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 36, Maspct = 9, Masize = 4, SoLuongTonKho = 5 },



                     new Data.SanPhamSize { MaSanPhamSize = 37, Maspct = 10, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 38, Maspct = 10, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 39, Maspct = 10, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 40, Maspct = 10, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 41, Maspct = 11, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 42, Maspct = 11, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 43, Maspct = 11, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 44, Maspct = 11, Masize = 4, SoLuongTonKho = 5 },



                     new Data.SanPhamSize { MaSanPhamSize = 45, Maspct = 12, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 46, Maspct = 12, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 47, Maspct = 12, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 48, Maspct = 12, Masize = 4, SoLuongTonKho = 5 },



                     new Data.SanPhamSize { MaSanPhamSize = 49, Maspct = 13, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 50, Maspct = 13, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 51, Maspct = 13, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 52, Maspct = 13, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 53, Maspct = 14, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 54, Maspct = 14, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 55, Maspct = 14, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 56, Maspct = 14, Masize = 4, SoLuongTonKho = 5 },



                     new Data.SanPhamSize { MaSanPhamSize = 57, Maspct = 15, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 58, Maspct = 15, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 59, Maspct = 15, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 60, Maspct = 15, Masize = 4, SoLuongTonKho = 5 },




                     new Data.SanPhamSize { MaSanPhamSize = 61, Maspct = 16, Masize = 1, SoLuongTonKho = 5 },
                    new Data.SanPhamSize { MaSanPhamSize = 62, Maspct = 16, Masize = 2, SoLuongTonKho = 6 },
                    new Data.SanPhamSize { MaSanPhamSize = 63, Maspct = 16, Masize = 3, SoLuongTonKho = 5 },
                   new Data.SanPhamSize { MaSanPhamSize = 64, Maspct = 16, Masize = 4, SoLuongTonKho = 5 }

                    );




                //  You can use the DbSet<T>.AddOrUpdate() helper extension method
                //  to avoid creating duplicate seed data.
      

    //  You can use the DbSet<T>.AddOrUpdate() helper extension method
    //  to avoid creating duplicate seed data.
}
    }
}
