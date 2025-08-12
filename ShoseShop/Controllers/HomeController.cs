using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.Repositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoseShop.Controllers
{
    public class HomeController : Controller
    {
      
            private readonly ShoesContext _db;
            private readonly IKhuyenMai kmRepo;
            private readonly IChiTietSanPham spRepo;

        // TẠO CONSTRUCTOR RỖNG VÀ TỰ KHỞI TẠO CÁC ĐỐI TƯỢNG
            public HomeController()
            {
                _db = new ShoesContext();
                kmRepo = new KhuyenMaiRepo(_db);
               spRepo = new ChiTietSanphamRepo(_db);
            }

            public ActionResult Index()
            {
                var banners = _db.Banner.Where(b => b.Hoatdong).ToList();
                int checkKm = kmRepo.GetAllKhuyenMaiToday("", 0, 0, 0, 0, -1).Count == 0 ? 0 : 1;
                Session["Khuyenmai"] = checkKm;

                return View(banners);
            }

            public ActionResult Zalo()
            {
                return View();
            }


      
        public ActionResult HomeProductSlider(int trangthai)
        {
          
            // Gọi lại phương thức của repository để lấy dữ liệu
            List<SanPhamHomeViewModel> spViewHome = spRepo.HomeSanPham(trangthai);

            // Trả về Partial View tương ứng với dữ liệu
            return PartialView("HomeProductSlider", spViewHome);
        }

        public ActionResult HomeProductSlider2(int trangthai)
        {

            // Gọi lại phương thức của repository để lấy dữ liệu
            List<SanPhamHomeViewModel> spViewHome = spRepo.HomeSanPham(trangthai);

            // Trả về Partial View tương ứng với dữ liệu
            return PartialView("HomeProductSlider2", spViewHome);
        }



        public ActionResult PdSliderSale()
        {
            ViewBag.Ngaykt = kmRepo.getNgayktKmToday();
            List<SanphamViewModel> spView = new List<SanphamViewModel>();

            List<KhuyenMai> kmList = kmRepo.GetAllKhuyenMaiToday("", 0, 0, 0, 0, -1).OrderByDescending(x => x.PhanTramGiam).ToList();
            foreach (KhuyenMai km in kmList)
            {
                List<SanphamViewModel> dongspViewTemp = km.SanPhams.Select(x => new SanphamViewModel
                {
                    sanphams = x,
                    Phantramgiam = km.PhanTramGiam
                }).ToList();
                spView = spView.Concat(dongspViewTemp).ToList();
            }
            return PartialView("PdSliderSale",(spView));
        }



        
    }
    }