using Newtonsoft.Json;
using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoseShop.Controllers
{
    public class TrackingOrderController : Controller
    {
      
            private readonly IPhieuMua _phieuMuaRepo;

            public TrackingOrderController(IPhieuMua phieuMuaRepo)
            {
                _phieuMuaRepo = phieuMuaRepo;
            }


            public ActionResult Tracking()
            {
                return View();
            }


            public ActionResult TrackingDetails(int orderId)
            {
                var order = _phieuMuaRepo.GetOrderById(orderId);
                if (order == null)
                {
                    return PartialView("NotFound");  // Ensure you have a view to handle the not found case.
                }

                return PartialView("TrackingDetails", order);
            }
        }
    }
