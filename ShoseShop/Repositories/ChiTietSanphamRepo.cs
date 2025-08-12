using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
namespace ShoseShop.Repositories
{
	public class ChiTietSanphamRepo : IChiTietSanPham
	{
		ShoesContext __db;
		public ChiTietSanphamRepo(ShoesContext _db)
		{
			__db = _db;
		}

		public ChiTietSanPham Getsanphamct(int masp)
		{
            ChiTietSanPham sp = __db.ChiTietSanPhams.FirstOrDefault(x => x.MaChiTietSP == masp);
			return sp;

		}
        public ChiTietSanphamViewModel HienThiSanpham(int maSanPham, int maspct)
        {
            DateTime today = DateTime.Now.Date;
            SanPham dongsp = __db.Sanphams.FirstOrDefault(x => x.MaSanPham == maSanPham);

            KhuyenMai KmTodayThatdsp = __db.Khuyenmais.Include(x => x.SanPhams)
                                            .Include(d => d.ChiTietKhuyenMais)
                                            .FirstOrDefault(a => a.NgayBatDau <= today && today < a.NgayKetThuc && a.SanPhams.Any(k => k.MaSanPham == maSanPham));

            dongsp.KhuyenMais.Add(KmTodayThatdsp);

            List<ChiTietSanPham> sp = __db.ChiTietSanPhams.
                Where(x => x.MaChiTietSP != maspct && x.MaSP == maSanPham)
                .ToList();
            ChiTietSanPham ctFirst = __db.ChiTietSanPhams.FirstOrDefault(x => x.MaChiTietSP == maspct);

            ctFirst.MaMauNavigation = __db.Maus.FirstOrDefault(x => x.MaMau == ctFirst.MaMau);
            sp.Insert(0, ctFirst);

            ChiTietSanphamViewModel pDetail = new ChiTietSanphamViewModel
            {
                sanphams = dongsp,
                sanphamct = sp,
                tenSize = __db.Sizes
                    .Select(x => x.TenSize).ToList(),
                slton = __db.Sanphamsizes
                        .Where(x => x.Maspct == maspct)
                        .Select(x => x.SoLuongTonKho).ToList()
            };

            return pDetail;
        }

        public List<SanPhamHomeViewModel> HomeSanPham(int trangthai)
        {
            DateTime today = DateTime.Now.Date;
            //List<KhuyenMai> allkmToday = __db.Khuyenmais.Include(x => x.SanPhams)
            //                                .ThenInclude(d => d.ChiTietSanPhams).Where(x => x.NgayBatDau >= today && x.NgayKetThuc <= today)
            //                                .ToList();

            List<KhuyenMai> allkmToday = __db.Khuyenmais.Include("SanPhams.ChiTietSanPhams")
  
                .Where(x => x.NgayBatDau <= today && x.NgayKetThuc >= today).ToList();

            List<ChiTietSanPham> sp = __db.ChiTietSanPhams.Where(x => x.TrangThai == (ChiTietSanPham.TrangThaiEnum)trangthai)
                                            .Include(x => x.MaSPNavigation)
                                            .Include(x => x.MaMauNavigation).ToList();
            List<SanPhamHomeViewModel> spViewHome = new List<SanPhamHomeViewModel>();

            foreach (var sanphamct in sp)
            {

                bool check = true;
                foreach (KhuyenMai km in allkmToday)
                {
                    if (km.SanPhams.FirstOrDefault(x => x.ChiTietSanPhams.Contains(sanphamct)) != null)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    spViewHome.Add(new SanPhamHomeViewModel
                    {
                        spct = sanphamct,
                        phantramgiam = 0
                    });
                }
            }

            return spViewHome;
        }
        
        public FavouriteProductsItem GetFavProById(int id)
		{
			ChiTietSanPham sp = __db.ChiTietSanPhams.FirstOrDefault(x => x.MaChiTietSP == id);
			SanPham dsp = __db.Sanphams.FirstOrDefault(x => x.MaSanPham == sp.MaSP);
			DateTime today = DateTime.Now;
			int phantramgiam = __db.Khuyenmais.FirstOrDefault(x => x.SanPhams.Contains(dsp) && x.NgayBatDau <= today && x.NgayKetThuc >= today)?.PhanTramGiam ?? 0;
			FavouriteProductsItem favPro = new FavouriteProductsItem
			{
				Id= id,
				Madongsp = dsp.MaSanPham,
				Tensp =dsp.TenSanPham,
				Hinhanh = sp.AnhDaiDien,
				Gia = dsp.GiaSanPham,
				Phantramgiam = phantramgiam
			};

			return favPro;
		}
	}
}
