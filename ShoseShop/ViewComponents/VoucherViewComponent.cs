using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace ShoseShop.ViewComponents
{
    public class VoucherViewComponent : ViewComponent
    {
        IVoucher voucherRepo;

        public VoucherViewComponent(IVoucher voucherRepo)
        {
            this.voucherRepo = voucherRepo;
        }

        public IViewComponentResult Invoke()
        {
            List<Voucher> AllvoucherToday = voucherRepo.getAllVoucherToday();
            return View(AllvoucherToday);
        }

    }
}