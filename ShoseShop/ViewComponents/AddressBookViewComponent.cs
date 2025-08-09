using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;


namespace ShoseShop.ViewComponents
{
    public class AddressBookViewComponent: ViewComponent
    {

        IAddressNoteBook addressRepo; IKhachHang khRepo;
        public AddressBookViewComponent(IAddressNoteBook addressRepo, IKhachHang khRepo)
        {
            this.addressRepo = addressRepo;
            this.khRepo = khRepo;
        }
        public IViewComponentResult Invoke()
        {
            List<Tinh> tinhs = addressRepo.GetTinhs();
            List<SelectListItem> selectTinh = tinhs.Select(x => new SelectListItem
            {
                Value = x.Matinh.ToString(),
                Text = x.Tentinh
            }).ToList();
            ViewBag.TinhSelect = selectTinh;

            string Email = HttpContext.Session.GetString("Email") ?? "truongmyhoa561@gmail.com";
            KhachHang kh = khRepo.GetCurrentKh(Email);

            SodiachiProfileViewModel sdcprofile = new SodiachiProfileViewModel()
            {
                sdc = addressRepo.GetAllAddressNoteByMaKH(kh.MaKhachHang),
                currentKhName = kh.TenKhachHang,
                currentPhone = kh.Phone
            };
            return View(sdcprofile);
        }
    
     }
}