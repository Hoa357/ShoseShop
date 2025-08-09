using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoseShop.Controllers
{
    public class HomeController : Controller
    {
           private readonly   ShoesDbContext _db; IKhuyenMai kmRepo;
            public HomeController(ShoesDbContext db, IKhuyenMai km)
            {
                _db = db;
                this.kmRepo = km;
            }
            public ActionResult Index()
            {
                var banners = _db.Banner.Where(b => b.Hoatdong).ToList();
                int checkKm = kmRepo.GetAllKhuyenMaiToday("", "", 0, 0, 0, -1).Count == 0 ? 0 : 1;

               Session["Khuyenmai"] = checkKm;

            return View(banners);
            }


            public ActionResult Zalo()
            {
                return View();
            }

        }
    }