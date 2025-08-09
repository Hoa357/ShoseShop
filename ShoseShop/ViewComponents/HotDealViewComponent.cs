using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace ShoseShop.ViewComponents
{
    public class HotDealViewComponent : ViewComponent
    {
        ShoesDbContext _context;
        public HotDealViewComponent(ShoesDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}