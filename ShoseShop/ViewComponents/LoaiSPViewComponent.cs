using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace ShoseShop.ViewComponents
{
    public class LoaiSPViewComponent : ViewComponent
    {
        ShoesDbContext _context;
        public LoaiSPViewComponent(ShoesDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Loai> l1 = _context.Loais.ToList();
            return View(l1);
        }

    }
}