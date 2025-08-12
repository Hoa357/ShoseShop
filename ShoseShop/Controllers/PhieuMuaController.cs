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
    public class PhieuMuaController : Controller
    {
       
            IKhachHang khRepo; IPhuongthucthanhtoan ptttRepo; ISanPhamSize spsizeRepo; IPhieuMua pmRepo; IEmailSender emailSender ; IAddressNoteBook addressRepo;
            IVoucher voucherRepo; IChiTietSanPham sanphamctRepo; ISanPham sanphamRepo; IMau mauRepo; IKhuyenMai kmRepo; ISanPhamSize tkhoRepo;
            public PhieuMuaController(IKhachHang kh, IPhuongthucthanhtoan pt, ISanPhamSize sizesp, IPhieuMua pm2, IEmailSender emailrepo,
                IAddressNoteBook addressrepo, IVoucher voucherrepo, IChiTietSanPham sanphamctrepo, ISanPham sanphamrepo, IMau maurepo, IKhuyenMai kmrepo
                , ISanPhamSize tkhorepo)
            {
                 khRepo = kh;
                ptttRepo = pt;
            spsizeRepo = sizesp;
            pmRepo = pm2;
            emailSender = emailrepo;
            addressRepo = addressrepo;
            voucherRepo = voucherrepo;
            sanphamctRepo = sanphamctrepo;
            sanphamRepo = sanphamrepo;
            mauRepo = maurepo;
            kmRepo  = kmrepo;
            tkhoRepo = tkhorepo;
            }

            public ActionResult ThanhToan()
            {
               

            // Lấy chuỗi JSON từ Session
            string cartJson = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartJson))
            {
               shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartJson);
            }

           
            if (shoppingCart.Count == 0) 
            {
                // Nếu giỏ hàng rỗng, chuyển hướng
                return RedirectToAction("SanPhamTheoLoai", "SanPham", new { maloai = -1 });
            }

            string emailkh = (Session["Email"] as string) ?? "";
            

            KhachHang kh = khRepo.GetCurrentKh(emailkh);
            List<ShoppingCartItem> cartItem = shoppingCart ?? new List<ShoppingCartItem>();

                decimal total = 0;
                foreach (ShoppingCartItem shopcartItem in cartItem)
                {
                    decimal tiensp;
                    if (shopcartItem.PhanTramGiam != 1)
                    {
                        tiensp = shopcartItem.Quantity * (shopcartItem.GiaGoc - (shopcartItem.GiaGoc * shopcartItem.PhanTramGiam / 100));
                    }
                    else
                    {
                        tiensp = shopcartItem.Quantity * shopcartItem.GiaGoc;
                    }
                    total += tiensp;
                }
                decimal coin = Math.Ceiling(total / 100000);
                coin = coin * 1000;
                PhieuMuaViewModel phieuMua = new PhieuMuaViewModel()
                {
                    listcartItem = cartItem,
                    khInfo = kh,
                    HoTen = kh.TenKhachHang ?? "",
                    Sdt = kh.Phone ?? "",
                    Email = kh?.Email ?? "",
                    Mapttt = 0,
                    voucherList = voucherRepo.getAllVoucherToday(),
                    //  SoDiaChis = addressRepo.GetAllAddressNoteByMaKH(kh.Makh),
                    SoDiaChis = kh != null ? addressRepo.GetAllAddressNoteByMaKH(kh.MaKhachHang) : addressRepo.GetAllAddressNote(),
                    totalCost = total,
                    tempCost = total,
                    Choosenvoucher = null,
                    coinGet = coin,
                    discountMoney = 0,
                    coinChoosen = 0,
                    coinApply = 0
                };

                ViewBag.MethodPurchase = ptttRepo.GetAllPttt();
                CreateSelectAddress();
                return View(phieuMua);
            }

            public ActionResult ChooseVoucherAndApplyXu(string mavoucher, bool? Boolcheck, decimal? AmountXu, int? changeAmount, bool? Apply, decimal? AmountApply)
            {
            string emailkh = (Session["Email"] as string) ?? "";
            decimal voucherApplyCode = 0, maxDiscount = 0;
                List<Voucher> voucherList = voucherRepo.getAllVoucherToday();
                KhachHang kh = khRepo.GetCurrentKh(emailkh);

            // Lấy chuỗi JSON từ Session
            string cartJson = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartJson))
            {
                shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartJson);
            }

            List<ShoppingCartItem> cartItem = shoppingCart ?? new List<ShoppingCartItem>();
                decimal total = 0;

                foreach (ShoppingCartItem shopcartItem in cartItem)
                {
                    decimal tiensp;
                    if (shopcartItem.PhanTramGiam != 1)
                    {
                        tiensp = shopcartItem.Quantity * (shopcartItem.GiaGoc - (shopcartItem.GiaGoc * shopcartItem.PhanTramGiam / 100));
                    }
                    else
                    {
                        tiensp = shopcartItem.Quantity * shopcartItem.GiaGoc;
                        voucherApplyCode += tiensp;
                    }
                    total += tiensp;
                }
                decimal tempCost = total;

                Voucher voucher = new Voucher();
                if (!string.IsNullOrEmpty(mavoucher))
                {
                    voucher = voucherRepo.GetVoucherByCode(mavoucher);
                    if (voucherApplyCode >= ((decimal)voucher.GiaToiThieu))
                    {
                        voucherList.FirstOrDefault(x => x.MaVoucher == mavoucher).SoLuong -= 1;
                    total -= ((decimal)voucher.GiaToiDa);
                        ViewBag.voucherCode = mavoucher;
                    maxDiscount += ((decimal)voucher.GiaToiDa);
                    }
                    else
                    {
                        string messageError = "Mã giảm  " + mavoucher + " chỉ áp dụng cho đơn hàng " + voucher.GiaToiThieu.ToString("#,##0") + " VND";
                        ViewBag.voucherError = messageError;
                    }
                }
                if (changeAmount == 1)
                {
                    if (AmountXu <= kh.TongXu - 10000m)
                    {
                        AmountXu += 10000m;
                    }
                }
                else if (changeAmount == 2)
                {
                    if (AmountXu >= 10000m)
                    {
                        AmountXu -= 10000m;
                    }
                }
                ViewBag.xuTemp = AmountXu;
                ViewBag.BoolCheck = Boolcheck;
                if (Apply == true)
                {
                    AmountApply = AmountXu;
                }

                ViewBag.AmountApply = AmountApply;
                total -= AmountApply ?? 0;
                maxDiscount += AmountApply ?? 0;
                ViewBag.voucherDiscount = maxDiscount;
                decimal coin = Math.Ceiling(total / 100000);
                coin = coin * 1000;
                PhieuMuaViewModel phieuMua = new PhieuMuaViewModel()
                {
                    listcartItem = shoppingCart ?? new List<ShoppingCartItem>(),
                    khInfo = kh,
                    Mapttt = 0,
                    voucherList = voucherList,
                    Choosenvoucher = string.IsNullOrEmpty(mavoucher) ? null : voucher,
                    SoDiaChis = addressRepo.GetAllAddressNote(),
                    totalCost = total,
                    tempCost = tempCost,
                    coinGet = coin,
                    discountMoney = maxDiscount,
                    coinChoosen = AmountXu ?? 0,
                    coinApply = AmountApply ?? 0,
                };



                return PartialView("_PartialViewPriceDetail", phieuMua);
            }

         
            [HttpPost]
            public ActionResult ThanhToan(PhieuMuaViewModel phieuMua)
            {
            // Lấy chuỗi JSON từ Session
            string cartJson = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartJson))
            {
                shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartJson);
            }

            // Lấy giỏ hàng từ Session
            phieuMua.listcartItem = shoppingCart ?? new List<ShoppingCartItem>();

                // Kiểm tra giỏ hàng rỗng
                if (!phieuMua.listcartItem.Any())
                {
                    ModelState.AddModelError("", "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm trước khi thanh toán.");
                }

                // Lấy thông tin khách hàng
                string emailkh = (Session["Email"] as string) ?? "";
            phieuMua.khInfo = khRepo.GetCurrentKh(emailkh);

                // Tái tạo danh sách dropdown
                CreateSelectAddress();
                ViewBag.MethodPurchase = ptttRepo.GetAllPttt();

                // Nếu maTinh có giá trị, tạo danh sách Quận
                if (phieuMua.maTinh.HasValue && phieuMua.maTinh.Value != 0)
                {
                    ViewBag.QuanSelect = addressRepo.GetQuanList(phieuMua.maTinh.Value)
                        .Select(x => new SelectListItem { Value = x.MaQuan.ToString(), Text = x.TenQuan }).ToList();
                }
                else
                {
                    ViewBag.QuanSelect = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Chọn quận/huyện" } };
                }

                // Nếu maQuan có giá trị, tạo danh sách Phường
                if (phieuMua.maQuan.HasValue && phieuMua.maQuan.Value != 0)
                {
                    ViewBag.PhuongSelect = addressRepo.GetPhuongList(phieuMua.maQuan.Value)
                        .Select(x => new SelectListItem { Value = x.MaPhuong.ToString(), Text = x.TenPhuong }).ToList();
                }
                else
                {
                    ViewBag.PhuongSelect = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Chọn phường/xã" } };
                }

                // Gán các thông tin khác
                phieuMua.SoDiaChis = addressRepo.GetAllAddressNote();
                phieuMua.voucherList = voucherRepo.getAllVoucherToday();
                ViewBag.xuTemp = phieuMua.coinChoosen;
                ViewBag.AmountApply = phieuMua.coinApply;
                ViewBag.BoolCheck = phieuMua.coinChoosen != 0;

                try
                {
                    // Lưu thông tin đơn hàng
                    spsizeRepo.MinusSanPhamSize(phieuMua);
                    int mapm = pmRepo.AddPhieuMua(phieuMua);

                    // Xóa giỏ hàng khỏi session
                    HttpContext.Session.Remove("Cart");

                    // Gửi email cho quản trị viên
                    string receiver = "truongmyhoa561@gmail.com";
                    string subject = "Một đơn hàng mới";
                    string message = $"Mã vận đơn: {mapm}";
                    emailSender.SendEmail(receiver, subject, message);

                    // Gửi email xác nhận cho khách hàng
                    if (!string.IsNullOrEmpty(emailkh))
                    {
                        string customerMessage = $"Xin chào {phieuMua.HoTen},\n\n"
                                               + $"Cảm ơn bạn đã đặt hàng tại ShoesStore!\n"
                                               + $"Mã đơn hàng của bạn là: {mapm}\n"
                                               + $"Tổng số tiền thanh toán: {phieuMua.totalCost.ToString("#,##0")}₫.\n"
                                               + $"Chúng tôi sẽ liên hệ để xác nhận đơn hàng trong thời gian sớm nhất.\n\n"
                                               + $"Trân trọng,\nĐội ngũ ShoesStore";

                        emailSender.SendEmail(emailkh, "Xác nhận đơn hàng", customerMessage);
                    }

                    return RedirectToAction("Confirmation", "PhieuMua", new { mapm = mapm });
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(phieuMua);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi xử lý đơn hàng. Vui lòng thử lại.");
                    return View(phieuMua);
                }
            }
            public ActionResult AddressNoteBook()
            {

                PhieuMuaViewModel pm = new PhieuMuaViewModel();
                List<Tinh> tinhList = addressRepo.GetTinhList();
                List<SelectListItem> selectTinh = tinhList.Select(x => new SelectListItem
                {
                    Value = x.Matinh.ToString(),
                    Text = x.Tentinh
                }).ToList();

                ViewBag.SelectTinh = selectTinh;

                KhachHang kh = khRepo.GetCurrentKh("truongmyhoa561@gmail.com");
                pm.khInfo = kh;

                pm.SoDiaChis = addressRepo.GetAllAddressNote();

                return View(pm);
            }

            public ActionResult AddAddress(int province, int district, int ward, string address, int makh, string tennguoinhan, string sdtnguoinhan)
            {
                addressRepo.AddAddressNote(province, district, ward, address, makh, tennguoinhan, sdtnguoinhan);
                return RedirectToAction("AddressNoteBook");
            }

            public ActionResult GetDistinctsByProvince(int provinceId)
            {
                List<Quan> quanList = addressRepo.GetQuanList(provinceId);
                List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
                {
                    Value = x.MaQuan.ToString(),
                    Text = x.TenQuan
                }).ToList();
                return Json(selectQuan);
            }

            public ActionResult GetWardByDistrict(int districtId)
            {
                List<Phuong> phuongList = addressRepo.GetPhuongList(districtId);
                List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
                {
                    Value = x.MaPhuong.ToString(),
                    Text = x.TenPhuong
                }).ToList();
                return Json(selectPhuong);
            }

            public ActionResult Confirmation(int mapm)
            {
                return View(mapm);
            }

            public void CreateSelectAddress()
            {
                List<Tinh> tinhs = addressRepo.GetTinhList();
                List<SelectListItem> selectTinh = tinhs.Select(x => new SelectListItem
                {
                    Value = x.Matinh.ToString(),
                    Text = x.Tentinh
                }).ToList();
                ViewBag.TinhSelect = selectTinh;
                ViewBag.QuanSelect = null;
                ViewBag.PhuongSelect = null;
            }

            public ActionResult UpdateAddress(int masdc)
            {

                SoDiaChi sdc = addressRepo.GetSoDiaChi(masdc);
                int maTinh = addressRepo.GetMaTinh(sdc.DiaChi.Split(',')[1].TrimStart());
                int maQuan = addressRepo.GetMaQuan(sdc.DiaChi.Split(',')[2].TrimStart());
                int maPhuong = addressRepo.GetMaPhuong(sdc.DiaChi.Split(',')[3].TrimStart());

                List<Tinh> tinhs = addressRepo.GetTinhList();
                List<SelectListItem> selectTinh = tinhs.Select(x => new SelectListItem
                {
                    Value = x.Matinh.ToString(),
                    Text = x.Tentinh
                }).ToList();

                List<Quan> quanList = addressRepo.GetQuanList(maTinh);
                List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
                {
                    Value = x.MaQuan.ToString(),
                    Text = x.TenQuan
                }).ToList();


                List<Phuong> phuongList = addressRepo.GetPhuongList(maQuan);
                List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
                {
                    Value = x.MaPhuong.ToString(),
                    Text = x.TenPhuong
                }).ToList();


                UpdateAddressViewModel addView = new UpdateAddressViewModel()
                {
                    Masdc = masdc,
                    Tennguoinhan = sdc.TenNguoiNhan,
                    Sdtnguoinhan = sdc.SoDTNguoiNhan,
                    Diachi = sdc.DiaChi.Split(',')[0],
                    MaTinh = maTinh,
                    MaQuan = maQuan,
                    MaPhuong = maPhuong,
                    tinhSelect = selectTinh,
                    quanSelect = selectQuan,
                    phuongSelect = selectPhuong
                };
                return Json(addView);
            }

     

        public ActionResult UpdateAddressFinal(int masdc, string hoten, string sdt, string diachi, int matinh, int maquan, int maphuong)
            {
                addressRepo.UpdateSDC(masdc, hoten, sdt, diachi, matinh, maquan, maphuong);
                List<SoDiaChi> sdcList = addressRepo.GetAllAddressNote();

                return PartialView("AddressBookPartialView", sdcList);

            }

            public ActionResult DeleteAddress(int masdc)
            {
                addressRepo.DeleteSDC(masdc);
            string emailkh = (Session["Email"]  as string ) ?? "";
                KhachHang kh = khRepo.GetCurrentKh(emailkh);
                List<SoDiaChi> sdcList = addressRepo.GetAllAddressNoteByMaKH(kh.MaKhachHang);

                return PartialView("AddressBookPartialView", sdcList);
            }

            public bool Check(PhieuMuaViewModel phieuMua)
            {
                bool check = false;
             
                if (phieuMua.maTinh == 0)
                {
                    ModelState.AddModelError("maTinh", "Vui lòng chọn thành phố/tỉnh");
                    check = true;
                }
                if (phieuMua.maQuan == 0)
                {
                    ModelState.AddModelError("maQuan", "Vui lòng chọn quận/huyện");
                    check = true;
                }
                if (phieuMua.maPhuong == 0)
                {
                    ModelState.AddModelError("maPhuong", "Vui lòng chọn phường/xã");
                    check = true;
                }
                if (phieuMua.Mapttt == null || phieuMua.Mapttt == 0)
                {
                    ModelState.AddModelError("Mapttt", "Please choose method purchase");
                    check = true;
                }
                return check;
            }

            public ActionResult ChangeAddressPhieuMua(int maSoDiaChi)
            {
                SoDiaChi sdc = addressRepo.GetSoDiaChi(maSoDiaChi);
                string emailkh = (Session["Email"] )as string  ?? "";
                KhachHang kh = khRepo.GetCurrentKh(emailkh);

                var diachiParts = sdc.DiaChi.Split(',');
                if (diachiParts.Length < 4)
                {
                    // Trường hợp địa chỉ không hợp lệ
                    return Json(new { success = false, message = "Địa chỉ không hợp lệ" });
                }

                PhieuMuaViewModel pmview = new PhieuMuaViewModel()
                {
                    HoTen = sdc.TenNguoiNhan,
                    Sdt = sdc.SoDTNguoiNhan,
                    Diachi = diachiParts[0],
                    maTinh = addressRepo.GetMaTinh(diachiParts[1].TrimStart()),
                    maQuan = addressRepo.GetMaQuan(diachiParts[2].TrimStart()),
                    maPhuong = addressRepo.GetMaPhuong(diachiParts[3].TrimStart())
                };

                List<Quan> quanList = addressRepo.GetQuanList(pmview.maTinh.Value);
                List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
                {
                    Value = x.MaQuan.ToString(),
                    Text = x.TenQuan
                }).ToList();
                pmview.selectQuan = selectQuan;

                List<Phuong> phuongList = addressRepo.GetPhuongList(pmview.maQuan.Value);
                List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
                {
                    Value = x.MaPhuong.ToString(),
                    Text = x.TenPhuong
                }).ToList();
                pmview.selectPhuong = selectPhuong;

                return Json(pmview);
            }

            public ActionResult OrderDetail(int id)
            {
                var order = pmRepo.GetOrderById(id);
                if (order == null)
                {
                    return HttpNotFound();
                }

                return View(order);
            }




        }
    }

