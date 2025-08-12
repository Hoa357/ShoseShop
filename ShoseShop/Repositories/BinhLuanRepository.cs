using PagedList;
using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System.Collections.Generic;
using System.Linq;


namespace ShoesStore.Repositories
{
    public class BinhLuanRepository : IBinhLuan
    {
        private readonly ShoesContext __db;

        public BinhLuanRepository(ShoesContext _db)
        {
            __db = _db;
        }

        public IQueryable<BinhLuan> LayTatCa()
        {
            return __db.BinhLuans.OrderBy(x => x.NgayBinhLuan);
        }

        public void AddBinhLuan(BinhLuan bl)
        {
            __db.BinhLuans.Add(bl);
            __db.SaveChanges();
        }

        public CommentViewModel GetBlList(int masp,int page=1)
        {
			ChiTietSanPham sp = __db.ChiTietSanPhams.Find(masp);
			int MaSanPham = __db.Sanphams.FirstOrDefault(x => x.MaSanPham == sp.MaSP).MaSanPham;
            List<BinhLuan> listofComment = __db.BinhLuans
            .Where(objComment => objComment.MaSP == MaSanPham)
            .OrderByDescending(x => x.MaBinhLuan)
            .ToList();


            PagedList<BinhLuan> pageBl = new PagedList<BinhLuan>(listofComment, page, 3);
			CommentViewModel cmtView = new CommentViewModel
			{
				blPage = pageBl,
				overallStar = listofComment.Count()>0 ? listofComment.Average(x=>x.Rating) : 0,
				totalReview = listofComment.Count(),
				fiveStar = listofComment.Count(x => x.Rating==5),
				fourStar = listofComment.Count(x => x.Rating == 4),
				threeStar = listofComment.Count(x => x.Rating == 3),
				twoStar = listofComment.Count(x => x.Rating == 2),
				oneStar = listofComment.Count(x => x.Rating == 1),
			};

			return cmtView;
		}

	}
}
