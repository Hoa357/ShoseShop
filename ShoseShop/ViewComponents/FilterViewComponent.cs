using Microsoft.AspNetCore.Mvc;
using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewComponents
{
    public class FilterViewComponent : ViewComponent
    {
        IMau mauRepo;
        public FilterViewComponent(IMau m)
        {
            this.mauRepo = m;
        }

        public IViewComponentResult Invoke()
        {
            List<Mau> mList = mauRepo.GetMauList();
            return View(mList);
        }
    }
}