using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace ShoseShop.Repositories
{
	public class SanphamSizeRepo : ISanPhamSize
	{
		ShoesContext _db;
		public SanphamSizeRepo(ShoesContext _db)
		{
			this._db = _db;
		}

		public SanPhamSize GetSLTon(int masp, int masize)
		{
            SanPhamSize spsize = _db.Sanphamsizes.FirstOrDefault(x => x.MaSanPhamSize == masp && x.Masize == masize);
			return spsize;
		}

        public int GetMaspsize(int masp, string tensize)
		{
			int masize = _db.Sizes.FirstOrDefault(x=>x.TenSize == tensize).MaSize;
			int maspsize = _db.Sanphamsizes.FirstOrDefault(x=>x.MaSanPhamSize ==masp && x.Masize == masize).MaSanPhamSize;
			return maspsize;
		}

        public void MinusSanPhamSize(PhieuMuaViewModel pm)
		{
			List<SanPhamSize> spsize = new List<SanPhamSize>();

            foreach (ShoppingCartItem cartItem in pm.listcartItem)
			{
				spsize.Add(_db.Sanphamsizes
			   .FirstOrDefault(x => x.MaSanPhamSize == cartItem.Maspsize));
				spsize.Last().SoLuongTonKho -= cartItem.Quantity;
            }


            _db.SaveChanges();

        }

    }
}
