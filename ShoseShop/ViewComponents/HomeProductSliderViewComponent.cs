using Microsoft.AspNetCore.Mvc;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewComponents
{
    public class HomeProductSliderViewComponent : ViewComponent
    {

        IChiTietSanPham spCTRepo;
        public HomeProductSliderViewComponent(IChiTietSanPham spctRepo)
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