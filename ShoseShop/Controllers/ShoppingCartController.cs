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
    public class ShoppingCartController : Controller
    {
       
            IChiTietSanPham _sanphamctrepo;
            ISanPham _product;
            ISize _size;
            ISanPhamSize _tkho;
            IMau _mau; IKhuyenMai kmRepo;

         //   private readonly IVnPayService _vnPayService;

            public ShoppingCartController(IChiTietSanPham productDetail, ISanPham product, ISize sz, ISanPhamSize tkho, IMau mau, IKhuyenMai kmRepo /* IVnPayService vnPayService*/ )
            {
                _sanphamctrepo = productDetail;
                _product = product;
                _size = sz;
                _tkho = tkho;
                _mau = mau;
                this.kmRepo = kmRepo;
                //_vnPayService = vnPayService;
            }


            //[System.Web.Mvc.HttpPost]
            //public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
            //{
            //    var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            //    return Json(url);
            //}

            //[HttpGet]
            //public IActionResult VnPaySuccess([FromQuery] PaymentResponseModel response)
            //{


            //    return View("VnPaySuccess", response);
            //}

            //[HttpGet]
            //public IActionResult PaymentCallbackVnpay()
            //{
            //    var response = _vnPayService.PaymentExecute(Request.Query);
            //    if (response == null || response.VnPayResponseCode != "00")
            //    {
            //        // Thanh toán thất bại, chuyển hướng về giỏ hàng với thông báo lỗi
            //        TempData["Message"] = $"Lỗi thanh toán VNPAY: {response?.VnPayResponseCode}";
            //        return RedirectToAction("ViewCart");
            //    }

            //    // Thanh toán thành công
            //    // Xóa giỏ hàng khỏi session tại đây
            //    HttpContext.Session.Remove("Cart");

            //    // Chuyển hướng đến trang thành công với các tham số trên query string
            //    return RedirectToAction("VnPaySuccess", response);
            //    //return Json(response);
            //}



            public ActionResult ViewCart()
            {

            // Lấy chuỗi JSON từ Session
            string cartItems = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartItems))
            {
                shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartItems);
            }
            else
            {
                // Nếu không có dữ liệu trong Session, khởi tạo giỏ hàng rỗng
                shoppingCart = new List<ShoppingCartItem>();
            }


                return View(cartItems);
            }

            [Route("ShoppingCart/AddToCart/{id}/{tenSize}/{slton}")]
            public ActionResult AddToCart(int id, string tenSize, int slton)
            {
                ChiTietSanPham sp = _sanphamctrepo.Getsanphamct(id);

            // Lấy chuỗi JSON từ Session
            string cartItems = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartItems))
            {
                shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartItems);
            }
            else
            {
                // Nếu không có dữ liệu trong Session, khởi tạo giỏ hàng rỗng
                shoppingCart = new List<ShoppingCartItem>();
            }

            var existingCartItem = shoppingCart.FirstOrDefault(item => item.sanphamct.MaChiTietSP == id && item.Size == tenSize);

               if (existingCartItem != null)
                {
                    if (existingCartItem.Quantity <= slton - 1)
                    {
                        existingCartItem.Quantity += 1;
                    }
                    else
                    {
                        return RedirectToAction("HienThiSanPham", "SanPham", new { madongsanpham = sp.MaSP, maspct = sp.MaChiTietSP });
                    }
                }
                else
                {
                    SanPham dongSanPham = _product.GetSanpham(sp.MaSP);
                    Mau mau = _mau.GetMau(sp.MaMau.ToString());

                shoppingCart.Add(new ShoppingCartItem()
                    {
                        sanphamct = sp,
                        Name = dongSanPham.TenSanPham,
                        TenMau = mau.TenMau,
                        Quantity = 1,
                        tonkho = slton,
                        GiaGoc = (decimal)dongSanPham.GiaSanPham,
                        PhanTramGiam = kmRepo.GetKmProductToday(dongSanPham),
                        Size = tenSize,
                        Maspsize = _tkho.GetMaspsize(sp.MaChiTietSP, tenSize)
                    });
                }
                       
            Session["Cart"] = shoppingCart;

            return RedirectToAction("ViewCart");
        }

            public ActionResult IncreaseOne(int Masp, string size)
            {
            
            string cartJson = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartJson))
            {
                shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartJson);
            }
            else
            {
               // Nếu không có dữ liệu trong Session, khởi tạo giỏ hàng rỗng
                shoppingCart = new List<ShoppingCartItem>();
            }
             

            ShoppingCartItem shopCarteIncrease = shoppingCart
                        .FirstOrDefault(x => x.sanphamct.MaChiTietSP == Masp && x.Size == size);

                if (shopCarteIncrease.tonkho >= shopCarteIncrease.Quantity + 1)
                {
                    shopCarteIncrease.Quantity += 1;
                }

                Session["Cart"] = shoppingCart;
           
                return PartialView("PartialCartList", shoppingCart);


        }


        public ActionResult DecreaseOne(int Masp, string size)
            {

            string cartJson = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartJson))
            {
                shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartJson);
            }
            else
            {
                // Nếu không có dữ liệu trong Session, khởi tạo giỏ hàng rỗng
                shoppingCart = new List<ShoppingCartItem>();
            }
               
            ShoppingCartItem shopCarteDecrease = shoppingCart
                    .FirstOrDefault(x => x.sanphamct.MaChiTietSP == Masp && x.Size == size);

                shopCarteDecrease.Quantity -= 1;
                if (shopCarteDecrease.Quantity == 0)
                {
                  shoppingCart.Remove(shopCarteDecrease);
                }
            
            Session["Cart"] = shoppingCart;

            return PartialView("PartialCartList", shoppingCart);
        }

            public ActionResult Delete(int Masp, string tenSize)
            {

            string cartJson = Session["Cart"] as string;

            // Khởi tạo một giỏ hàng rỗng
            var shoppingCart = new List<ShoppingCartItem>();

            // Nếu có chuỗi JSON, chuyển nó về lại List
            if (!string.IsNullOrEmpty(cartJson))
            {
                shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cartJson);
            }
            else
            {
                // Nếu không có dữ liệu trong Session, khởi tạo giỏ hàng rỗng
                shoppingCart = new List<ShoppingCartItem>();
            }

            ShoppingCartItem shopCartNeedDelete = shoppingCart
                    .FirstOrDefault(x => x.sanphamct.MaChiTietSP == Masp && x.Size == tenSize);

                if (shopCartNeedDelete != null)
                {
                shoppingCart.Remove(shopCartNeedDelete);
                }
             Session["Cart"] = shoppingCart;

            return PartialView("PartialCartList", shoppingCart);

            }



        }
    }

