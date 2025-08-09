using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Microsoft.AspNetCore.Http;
namespace ShoseShop.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly ShoesDbContext _db;
        public UserProfileViewComponent(ShoesDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            string userEmail = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(userEmail))
            {
                return View(null);
            }

            var user = _db.Taikhoans
                            .Include(t => t.KhachHangs) 
                            .FirstOrDefault(x => x.Email == userEmail);


            if (user == null)
            {
                // Nếu không tìm thấy user, cũng trả về View rỗng
                return View(null);
            }


            return View(user.KhachHangs);
        }
    }


}