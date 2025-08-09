using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;

namespace ShoseShop.ViewComponents
{
    public class OrderHistoryViewComponent : ViewComponent
    {
        private readonly IPhieuMua _phieuMuaRepository;

        public OrderHistoryViewComponent(IPhieuMua phieuMuaRepository)
        {
            _phieuMuaRepository = phieuMuaRepository;
        }

        public IViewComponentResult Invoke()
        {
         
            string emailkh = ViewContext.HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(emailkh))
            {
                // Gán thông báo lỗi
                TempData["OrderHistoryMessage"] = "Vui lòng đăng nhập để xem lịch sử đơn hàng.";
                return View("Error"); // Trả về View thông báo lỗi nếu email không có trong Session

            }
         

                var orders = _phieuMuaRepository.GetOrderHistoryByEmail(emailkh);

                return View(orders);

        }
    }
}