using Newtonsoft.Json;
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
    public class SanPhamController : Controller
    {

            ShoesContext _db;
            IBinhLuan blRepo; 
            ISanPham dongspRepo; IChiTietSanPham spctRepo; IMau mauRepo; IKhachHang khRepo;
            public SanPhamController()
            {
                // Khởi tạo DbContext
                _db = new ShoesContext();

                blRepo = new BinhLuanRepository(_db);
                dongspRepo = new SanphamRepo(_db);
                spctRepo = new ChiTietSanphamRepo(_db);
                mauRepo = new MauRepo(_db);
                khRepo = new KhachhangRepo(_db);
            }
        public ActionResult SanPhamTheoLoai(string searchString, int maMau, int? sortGia, decimal? minPrice, decimal? maxPrice, int maLoai)
            {
                CreateData();

                ViewBag.maLoai = maLoai;
                ViewBag.sortGia1 = sortGia;
                ViewBag.maMau1 = maMau;
                ViewBag.searchString1 = searchString;
                ViewBag.minPrice1 = minPrice;
                ViewBag.maxPrice1 = maxPrice;
                TempData["AddCategoryId"] = "true";

                List<SanphamViewModel> dongspview = dongspRepo.GetSanPhamView(maMau, sortGia, searchString, minPrice, maxPrice, maLoai);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_PartialSanPhamTheoLoai", dongspview);
                }

                else
                {
                    return View(dongspview);
                }

            }



        public ActionResult HienThiSanpham1(int maSanPham, int maspct)
        {
            ChiTietSanphamViewModel pDetail = spctRepo.HienThiSanpham(maSanPham, maspct);
            Session["Masp"] = maspct;
            ViewBag.masp = maspct;

            return View(pDetail);
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

            public ActionResult AddComment(int rating, string SanPhamComment)
            {
                // Kiểm tra xem người dùng đã đăng nhập hay chưa
                if (Session["Email"]  == null)
                {
                    return RedirectToAction("Login", "Account");
                }

           
             int Masp = (Session["Masp"] as int?) ?? 0;
            
        
                // Lấy thông tin sản phẩm từ mã sản phẩm
                ChiTietSanPham spct = spctRepo.Getsanphamct(Masp);
                // Lấy thông tin người dùng đang đăng nhập
                string userEmail = (Session["Email"] as string) ?? "";
                var user = khRepo.GetCurrentKh(userEmail);

                if (user == null)
                {
                    return HttpNotFound();
                }

               
                int makh = user.MaKhachHang;

                
                BinhLuan objComment = new BinhLuan
                {
                    MaSP = Masp,
                    NoiDung = SanPhamComment,
                    NgayBinhLuan = DateTime.Now,
                    MaKH = makh,
                    Rating = rating
                };

                // Thêm bình luận vào cơ sở dữ liệu
                blRepo.AddBinhLuan(objComment);
                CommentViewModel cmtView = blRepo.GetBlList(Masp);
                return PartialView("PartialShowComment", cmtView);
            }

            public ActionResult FilterCommentPage(int page)
            {
                int Masp =  (Session["Masp"] as int?) ?? 0;
            CommentViewModel cmtView = blRepo.GetBlList(Masp, page);

                return PartialView("PartialShowComment", cmtView);
            }


        public ActionResult ShowComment(int masp, int? page) 
        {
            int pageNumber = page ?? 1;

            
            CommentViewModel cmtView = blRepo.GetBlList(masp, pageNumber);

            return View(cmtView);
        }


        public ActionResult LoaiSP()
        {
            IEnumerable<Loai> l1 = _db.Loais.ToList();
          

            return PartialView("LoaiSP", l1);
        }

    }
    }

