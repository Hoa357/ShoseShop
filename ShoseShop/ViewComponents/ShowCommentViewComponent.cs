using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewComponents
{
    public class ShowCommentViewComponent : ViewComponent
    {
        IBinhLuan blRepo;
        public ShowCommentViewComponent(IBinhLuan blRepo)
        {
            this.blRepo = blRepo;
        }

        public IViewComponentResult Invoke()
        {
            int Masp = HttpContext.Session.GetInt32("Masp") ?? 0;
            CommentViewModel cmtView = blRepo.GetBlList(Masp);

            TempData["Comment"] = "Vui lòng đăng nhập trước khi comment";
            return View(cmtView);
        }
    }
}