using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using ShoseShop.ViewModel;

namespace ShoseShop.ViewComponents
{
    public class PdSliderSaleViewComponent : ViewComponent
    {
        IKhuyenMai kmRepo;
        public PdSliderSaleViewComponent(IKhuyenMai kmRepo)
        {
            this.kmRepo = kmRepo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Ngaykt = kmRepo.getNgayktKmToday();
            List<SanphamViewModel> spView = new List<SanphamViewModel>();

            List<KhuyenMai> kmList = kmRepo.GetAllKhuyenMaiToday("", "", 0, 0, 0, -1).OrderByDescending(x => x.PhanTramGiam).ToList();
            foreach (KhuyenMai km in kmList)
            {
                List<SanphamViewModel> dongspViewTemp = km.SanPhams.Select(x => new SanphamViewModel
                {
                    sanpham = x,
                    Phantramgiam = km.PhanTramGiam
                }).ToList();
                spView = spView.Concat(dongspViewTemp).ToList();
            }
            return View(spView);
        }

    }
}