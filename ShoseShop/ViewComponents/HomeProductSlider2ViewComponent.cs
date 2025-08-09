using Microsoft.AspNetCore.Mvc;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace ShoseShop.ViewComponents
{
    public class HomeProductSlider2ViewComponent : ViewComponent
    {
        IChiTietSanPham spCTRepo;
        public HomeProductSlider2ViewComponent(IChiTietSanPham spctRepo)
        {
            this.spCTRepo = spctRepo;
        }

        public IViewComponentResult Invoke(int trangthai)
        {
            List<SanPhamHomeViewModel> spViewHome = spCTRepo.HomeSanPham(trangthai);
            return View(spViewHome);
        }
    }
}