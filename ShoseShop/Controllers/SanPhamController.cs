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
    public class SanPhamController : Controller
    {
        
            IBinhLuan blRepo;
            ISanPham dongspRepo; IChiTietSanPham spctRepo; IMau mauRepo; IKhachHang khRepo;
            public SanPhamController(ShoesDbContext context, ISanPham dongspRepo, IChiTietSanPham spctRepo, IMau m,
               IKhachHang khRepo, IBinhLuan blRepo)
            {
                this.dongspRepo = dongspRepo;
                this.spctRepo = spctRepo;
                this.mauRepo = m;
                this.khRepo = khRepo;
                this.blRepo = blRepo;
            }
            public ActionResult SanPhamTheoLoai(string searchString, string maMau, int? sortGia, decimal? minPrice, decimal? maxPrice, int maLoai)
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

        

            public ActionResult HienThiSanpham(int madongsanpham, int maspct)
            {
                SanphamViewModel pDetail = spctRepo.HienThiSanpham(madongsanpham, maspct);
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

            // Lấy mã sản phẩm từ session
            // Lấy mã sản phẩm từ session
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

                // Lấy mã khách hàng
                int makh = user.MaKhachHang;

                // Kiểm tra độ dài của bình luận

                // Tạo đối tượng Binhluan từ thông tin đã lấy được
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
        }
    }

