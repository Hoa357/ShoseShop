using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace ShoseShop.ViewComponents
{
    public class KhuyenMaiViewComponent : ViewComponent
    {
        IKhuyenMai kmRepo;
        public KhuyenMaiViewComponent(IKhuyenMai kmRepo)
        {
            this.kmRepo = kmRepo;
        }

        public IViewComponentResult Invoke()
        {
            List<KhuyenMai> kmToday = kmRepo.GetAllKhuyenMaiToday("", "", 0, 0, 0, -1);
            return View(kmToday);
        }
    }
}