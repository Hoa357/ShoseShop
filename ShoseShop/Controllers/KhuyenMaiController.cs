using ShoesStore.Repositories;
using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.Repositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoseShop.Controllers
{
    public class KhuyenMaiController : Controller
    {

        private readonly IKhuyenMai kmRepo; IMau mauRepo; 
        private readonly ShoesContext _db;
        public KhuyenMaiController(IKhuyenMai kmRepo, IMau mauRepo)
        {
            this.kmRepo = kmRepo;
            this.mauRepo = mauRepo;
            _db = new ShoesContext();
        }

        public KhuyenMaiController()
        {
            _db = new ShoesContext();
            kmRepo = new KhuyenMaiRepo(_db);
            mauRepo = new MauRepo(_db);
        }
        public ActionResult SaleIndex(string searchStringKm, int maMau, int? sortGia, decimal? minPrice, decimal? maxPrice, int phantramgiam)
        {
            CreateData();
            ViewBag.phantramgiam = phantramgiam;
            ViewBag.sortGia1 = sortGia;
            ViewBag.maMau1 = maMau;
            ViewBag.searchString1 = searchStringKm;
            ViewBag.minPrice1 = minPrice;
            ViewBag.maxPrice1 = maxPrice;
            ViewBag.Ngaykt = kmRepo.getNgayktKmToday();
            List<SanphamViewModel> dongspView = new List<SanphamViewModel>();

            List<KhuyenMai> kmList = kmRepo.GetAllKhuyenMaiToday(searchStringKm, maMau, sortGia, minPrice, maxPrice, phantramgiam);
            foreach (KhuyenMai km in kmList)
            {
                List<SanphamViewModel> dongspViewTemp = km.SanPhams.Select(x => new SanphamViewModel
                {
                    sanphams = x,
                    Phantramgiam = phantramgiam > 1 ? phantramgiam : km.PhanTramGiam,
                }).ToList();
                dongspView = dongspView.Concat(dongspViewTemp).ToList();
            }


            dongspView = dongspView.Where(x => x.sanphams.ChiTietSanPhams != null).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialSanPhamTheoLoai", dongspView);
            }

            else
            {
                return View(dongspView);
            }

        }


        public ActionResult FindSaleProduct(string searchStringKm, int maMau, int? sortGia, decimal? minPrice, decimal? maxPrice, int phantramgiam)
        {
            CreateData();
            ViewBag.phantramgiam = phantramgiam;
            ViewBag.sortGia1 = sortGia;
            ViewBag.maMau1 = maMau;
            ViewBag.searchString1 = searchStringKm;
            ViewBag.minPrice1 = minPrice;
            ViewBag.maxPrice1 = maxPrice;
            ViewBag.Ngaykt = kmRepo.getNgayktKmToday();
            List<SanphamViewModel> dongspView = new List<SanphamViewModel>();

            List<KhuyenMai> kmList = kmRepo.GetAllKhuyenMaiToday(searchStringKm, maMau, sortGia, minPrice, maxPrice, phantramgiam);
            foreach (KhuyenMai km in kmList)
            {
                List<SanphamViewModel> dongspViewTemp = km.SanPhams.Select(x => new SanphamViewModel
                {
                    sanphams = x,
                    Phantramgiam = phantramgiam > 1 ? phantramgiam : km.PhanTramGiam,
                }).ToList();
                dongspView = dongspView.Concat(dongspViewTemp).ToList();
            }


            dongspView = dongspView.Where(x => x.sanphams.ChiTietSanPhams != null).ToList();
            return PartialView("_PartialSanPhamTheoLoai", dongspView);
        }


        public void CreateData()
        {
            List<SelectListItem> MauList = mauRepo.GetMauList().Select(x => new SelectListItem
            {
                Value = x.MaMau.ToString(),
                Text = x.TenMau
            }).ToList();
            ViewBag.MauList = MauList;
        }


        [ChildActionOnly] 
        public ActionResult KhuyenMai()
        {
            
            List<KhuyenMai> kmToday = kmRepo.GetAllKhuyenMaiToday("", 0, 0, 0, 0, -1);
            return PartialView("KhuyenMai", kmToday);
        }

    }
}

