using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ShoseShop.Controllers
{
    public class AccountController  : Controller
    {

            private readonly ShoesDbContext _db; IAddressNoteBook addressRepo; IKhachHang khRepo;

            public AccountController(ShoesDbContext db, IAddressNoteBook addressRepo, IKhachHang khRepo)
            {
                _db = db;
                this.addressRepo = addressRepo;
                this.khRepo = khRepo;
            }

         
        public ActionResult Register()
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            
            if (Session["Email"] != null && Session["Loaitk"] as string == "0")
            {
                // Nếu đã đăng nhập, chuyển hướng đến trang Home
                return RedirectToAction("Index", "Home");
            }

            // Nếu chưa đăng nhập, hiển thị trang đăng ký
            return View();
        }


        // POST: /Account/Register
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Register(RegisterViewModel model)
            {
                TaiKhoan existEmail = _db.Taikhoans.FirstOrDefault(x => x.Email == model.Khachhang.Email);
                if (existEmail != null)
                {
                    ModelState.AddModelError("Khachhang.Email", "Đã tồn tại email");
                    return View(model);
                }
                TaiKhoan newTk = new TaiKhoan
                {
                    Email = model.Khachhang.Email,
                    MatKhau = model.Taikhoan.MatKhau,
                    LoaiTK = 0
                };
                
                model.Taikhoan = newTk; // Đảm bảo email giữa Khachhang và Taikhoan khớp nhau
                model.Khachhang.EmailNavigation = newTk;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                // Lưu đối tượng Taikhoan vào cơ sở dữ liệu
                _db.Taikhoans.Add(model.Taikhoan);

                _db.Khachhangs.Add(model.Khachhang);
                _db.SaveChanges();

                TempData["SuccessMessage"] = "Đăng ký tài khoản thành công! Chào mừng bạn đến với ShoesShop.";
                // Chuyển hướng đến trang Home
                return RedirectToAction("Index", "Home");
            }

            // GET: /Account/Login
            public ActionResult Login(string backToPage = "")
            {
                if (!string.IsNullOrEmpty(backToPage))
                {
                    ViewBag.backToPage = backToPage;

                }

                // Kiểm tra xem người dùng đã đăng nhập hay chưa
                if (Session["Email"] == null && Session["Loaitk"] as string != "0")
                {
               
                    // Nếu chưa đăng nhập, hiển thị trang đăng nhập
                    return View();
                }
                else
                {
                    // Nếu đã đăng nhập, chuyển hướng đến trang Home
                    return RedirectToAction("Index", "Home");
                }
            }
            public ActionResult AddAddress(int province, int district, int ward, string address, string tennguoinhan, string sdtnguoinhan)
            {
                string email = Session["Email"] as string ?? "lephat@gmail.com";
               
                    KhachHang kh = khRepo.GetCurrentKh(email);
                   
                    addressRepo.AddAddressNote(province, district, ward, address, kh.MaKhachHang, tennguoinhan, sdtnguoinhan);
                    
                       List<SoDiaChi> sdc = addressRepo.GetAllAddressNoteByMaKH(kh.MaKhachHang);
                    return PartialView("AddressBookPartialView", sdc);
            }

            // POST: /Account/Login
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Login(TaiKhoan taikhoan, string backToPage = "")
            {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (Session["Email"] == null && Session["Loaitk"] as string != "0")
            {
                    // Tìm kiếm tài khoản trong cơ sở dữ liệu
                    var user = _db.Taikhoans.FirstOrDefault(x => x.Email == taikhoan.Email && x.MatKhau == taikhoan.MatKhau);

                    if (user != null)
                    {
                    // Lưu thông tin người dùng vào Session
                        Session["Email"] = user.Email;
                        Session["Loaitk"] = user.LoaiTK.ToString();
                    TempData["SuccessMessage"] = "Đăng nhập thành công!";

                        if (backToPage == "thanhtoan")
                        {
                            return RedirectToAction("ThanhToan", "PhieuMua");
                        }
                        if (backToPage == "comment")
                        {
                          
                        int masp = (Session["Masp"] as int?) ?? 0;
                        int madongsp = _db.Sanphams.Find(masp).MaSanPham;

                            return RedirectToAction("HienThiSanpham", "SanPham", new { madongsanpham = madongsp, masp = masp });
                        }
                      
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (backToPage != "")
                {
                    ViewBag.backToPage = backToPage;
                }

                return View(taikhoan);
            }

            // GET: /Account/Logout
            public ActionResult Logout()
            {
                // Xóa thông tin người dùng khỏi Session
                HttpContext.Session.Remove("Email");

                // Chuyển hướng đến trang Login
                return RedirectToAction("Login", "Account");
            }

            public ActionResult UserProfile()
            {
                // Kiểm tra xem người dùng đã đăng nhập hay chưa
                if (Session[("Email")] == null)
                {
                    // Nếu chưa đăng nhập, chuyển hướng đến trang Login
                    return RedirectToAction("Login", "Account");
                }

            // Lấy thông tin tài khoản khách hàng từ Session
            string userEmail = "";
            if (Session["Email"] != null)
            {
                userEmail = Session["Email"].ToString();
            }

            var user = _db.Taikhoans
                                .Include(t => t.KhachHangs)
                                .FirstOrDefault(x => x.Email == userEmail);

                if (user != null && user.KhachHangs != null)
                {
                    return View(user.KhachHangs);
                }

                // Nếu không tìm thấy thông tin, chuyển hướng về trang Home
                return RedirectToAction("Index", "Home");
            }


            public ActionResult Getuserprofile()
            {
            //  return ViewComponent("UserProfile");
            return PartialView("_UserProfile");
            }

            public ActionResult AddressBook()
            {
            // return ViewComponent("AddressBook");
            return PartialView("_AddressBook");
        }

            // GET: /Account/ChangeProfile
            public ActionResult ChangeProfile()
            {
            //   Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (Session[("Email")] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang Login
                return RedirectToAction("Login", "Account");
            }

            // Lấy thông tin tài khoản khách hàng từ Session

            string userEmail = "";
            if (Session["Email"] != null)
            {
                userEmail = Session["Email"].ToString();
            }

            var user = _db.Taikhoans
                                .Include(t => t.KhachHangs)
                                .FirstOrDefault(x => x.Email == userEmail);

                if (user != null)
                {
                    return View(user.KhachHangs);
                }

                // Nếu không tìm thấy thông tin, chuyển hướng về trang Home
                return RedirectToAction("Index", "Home");
            }

            //[ValidateAntiForgeryToken]
            public ActionResult ChangeProfileUpdate(string tenkh, string sdt, bool gioitinh, DateTime ngaysinh)
            {
           // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (Session["Email"] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang Login
                return RedirectToAction("Login", "Account");
            }

            // Lấy email của người dùng từ Session
            string userEmail = "";
            if (Session["Email"] != null)
            {
                userEmail = Session["Email"].ToString();
            }

            // Tìm thông tin khách hàng dựa trên email
            var customer = _db.Khachhangs.FirstOrDefault(kh => kh.Email == userEmail);

                if (customer != null)
                {
                    // Cập nhật thông tin khách hàng
                    customer.TenKhachHang = tenkh;
                    customer.Phone = sdt;
                    customer.GioiTinh = gioitinh;
                    customer.NgaySinh = ngaysinh;

                //   _db.Khachhangs.Update(customer);
                    _db.SaveChanges();

                    // Trả về JSON hoặc thông báo thành công
                    return PartialView("PartialViewProfileInfo", customer);
                }

                // Nếu không tìm thấy thông tin, trả về thông báo lỗi
                return Json(new { success = false });
            }

            public ActionResult OrderHistory()
            {
            // return ViewComponent("OrderHistory");
            return PartialView("_OrderHistory");
             }
        }
    }

