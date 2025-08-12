using ShoseShop.InterfaceRepositories;
using ShoseShop.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace ShoseShop.Repositories
{
	public class KhuyenMaiRepo : IKhuyenMai
	{

		ShoesContext _db;
		public KhuyenMaiRepo(ShoesContext _db)
		{
			this._db = _db;
		}
		
		public DateTime getNgayktKmToday()
		{
			DateTime today = DateTime.Now.Date;
			KhuyenMai km = _db.Khuyenmais.FirstOrDefault(x => x.NgayBatDau <= today && today < x.NgayKetThuc);
			
			return km.NgayKetThuc;
		}

		public List<KhuyenMai> GetAllKhuyenMaiToday(string searchString, int maMau, int? sortGia, decimal? minPrice, decimal? maxPrice,int Phantramgiam)
		{
			DateTime today = DateTime.Now.Date;
            List<KhuyenMai> kmList = _db.Khuyenmais
                    .Include(p => p.SanPhams.Select(c => c.ChiTietSanPhams))
                    .Where(x => x.NgayBatDau <= today && today < x.NgayKetThuc)
                    .ToList();

            if (maMau != 0)
			{
				kmList = kmList.Select(x => new KhuyenMai
				{
					SanPhams = x.SanPhams.Select(d => new SanPham
					{
						MaSanPham = d.MaSanPham,
						Maloai = d.Maloai,
						TenSanPham = d.TenSanPham,
						MoTa = d.MoTa,
						GiaSanPham = d.GiaSanPham,
						ChiTietSanPhams = _db.ChiTietSanPhams.Where(a => a.MaChiTietSP == d.MaSanPham && a.MaMau == maMau).ToList(),
						MaloaiNavigation = _db.Loais.FirstOrDefault(a => a.MaLoai == d.Maloai)
					}).ToList(),
					MaKhuyenMai = x.MaKhuyenMai,
					PhanTramGiam = x.PhanTramGiam,
					NgayBatDau = x.NgayBatDau,
					NgayKetThuc = x.NgayKetThuc,
                }).ToList();
			}

			kmList.ForEach(km => km.SanPhams = km.SanPhams.Where(d => d.ChiTietSanPhams.Count != 0 ).ToList());

			if (sortGia != null && sortGia != 0)
			{
				kmList.ForEach(km => km.SanPhams = sortGia == 1 ? km.SanPhams.OrderBy(d => d.GiaSanPham-(d.GiaSanPham*km.PhanTramGiam/100)).ToList() : km.SanPhams.OrderByDescending(d => d.GiaSanPham - (d.GiaSanPham * km.PhanTramGiam / 100)).ToList());
			}

			if (!string.IsNullOrEmpty(searchString))
			{
				kmList.ForEach(km => km.SanPhams = km.SanPhams.Where(d => d.TenSanPham.ToLower().Contains(searchString.ToLower())).ToList());
			}

			if (minPrice != null && maxPrice != 0 && maxPrice != null)
			{
				kmList.ForEach(km => km.SanPhams = km.SanPhams.Where(d => d.GiaSanPham - (d.GiaSanPham * km.PhanTramGiam / 100) >= minPrice && d.GiaSanPham - (d.GiaSanPham * km.PhanTramGiam / 100) <= maxPrice).ToList());
			}
			if(Phantramgiam > 0)
			{
				kmList = kmList.Where(x => x.NgayBatDau <= today && today < x.NgayKetThuc && x.PhanTramGiam == Phantramgiam).ToList();
			}
			return kmList;
		}


		

		

        public int GetKmProductToday(SanPham dsp)
        {
            DateTime today = DateTime.Now.Date;
            KhuyenMai km2 = _db.Khuyenmais.Include(p => p.SanPhams.Select(c => c.KhuyenMais))
                .FirstOrDefault(x => x.NgayBatDau <= today && today < x.NgayKetThuc && x.SanPhams.Contains(dsp));

            if (km2 != null)
            {
                return km2.PhanTramGiam;
            }
            return 1;
        }
    }
}
