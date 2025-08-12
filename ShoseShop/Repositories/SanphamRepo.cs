using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
namespace ShoesStore.Repositories
{
	public class SanphamRepo : ISanPham
	{
		ShoesContext _db;
		public SanphamRepo(ShoesContext _db)
		{
			this._db = _db;
		}

		public SanPham GetSanpham(int madong)
		{
			SanPham dongsp = _db.Sanphams.FirstOrDefault(x => x.MaSanPham == madong);
			return dongsp;
		}

        
        public List<SanphamViewModel> GetSanPhamView(int maMau, int? sortGia, string searchString, decimal? minPrice, decimal? maxPrice, int maLoai)
		{
			List<SanPham> dongsp;

			if (maMau != null)
			{
				dongsp = _db.Sanphams.Select(x => new SanPham
				{
					MaSanPham = x.MaSanPham,
					Maloai = x.Maloai,
					TenSanPham = x.TenSanPham,
					MoTa = x.MoTa,
					GiaSanPham = x.GiaSanPham,
					ChiTietSanPhams = _db.ChiTietSanPhams.Where(a => a.MaChiTietSP == x.MaSanPham && a.MaMau == maMau).ToList(),
					MaloaiNavigation = _db.Loais.FirstOrDefault(a => a.MaLoai == x.Maloai)
				}).ToList();
			}
			else
			{
				dongsp = _db.Sanphams
					.Select(x => new SanPham
					{
						MaSanPham = x.MaSanPham,
						Maloai = x.Maloai,
						TenSanPham = x.TenSanPham,
						MoTa = x.MoTa,
						GiaSanPham = x.GiaSanPham,
						ChiTietSanPhams = _db.ChiTietSanPhams.Where(a => a.MaChiTietSP == x.MaSanPham).ToList(),
						MaloaiNavigation = _db.Loais.FirstOrDefault(a => a.MaLoai == x.Maloai)
					}).ToList();
			}
			dongsp = dongsp.Where(x => x.ChiTietSanPhams.Count() != 0).ToList();

			if (maLoai >= 1)
			{
				dongsp = dongsp.Where(x => x.Maloai == maLoai).ToList();
			}

			DateTime today = DateTime.Now.Date;
			List<KhuyenMai> khuyenmais = _db.Khuyenmais.Include("SanPhams.KhuyenMais")

												.Where(x => x.NgayBatDau <= today && today < x.NgayKetThuc).ToList();

			List<SanphamViewModel> dspView = new List<SanphamViewModel>();

			foreach (SanPham dsp in dongsp)
			{
				int percent = 0;
				foreach (KhuyenMai dspKm in khuyenmais)
				{
					if (dspKm.SanPhams.Any(x => x.MaSanPham == dsp.MaSanPham))
					{
						percent = dspKm.PhanTramGiam;
					}
				}

				dspView.Add(new SanphamViewModel
				{
					sanphams = dsp,
					Phantramgiam = percent,
				});
				//Debug.WriteLine("Phan tram: "+percent);
			}
			if (sortGia != null && sortGia != 0)
			{

				if (sortGia == 1)
				{
					dspView = dspView.OrderBy(x => x.sanphams.GiaSanPham - (x.sanphams.GiaSanPham * x.Phantramgiam / 100)).ToList();
				}
				if (sortGia == 2)
				{
					dspView = dspView.OrderByDescending(x => x.sanphams.GiaSanPham - (x.sanphams.GiaSanPham * x.Phantramgiam / 100)).ToList();
				}

			}
			if (minPrice != null && maxPrice != 0 && maxPrice != null)
			{
				dspView = dspView.Where(x => x.sanphams.GiaSanPham - (x.sanphams.GiaSanPham * x.Phantramgiam / 100) >= minPrice &&
						x.sanphams.GiaSanPham - (x.sanphams.GiaSanPham * x.Phantramgiam / 100) <= maxPrice).ToList();
			}

			if (!string.IsNullOrEmpty(searchString))
			{
				dspView = dspView.Where(x => x.sanphams.TenSanPham.ToLower().Contains(searchString.ToLower())).ToList();
			}

			return dspView;
		}
	}
}
