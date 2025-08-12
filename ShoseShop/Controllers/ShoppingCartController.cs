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
                        return RedirectToAction("HienThiSanPham", "SanPham", new { maSanPham = sp.MaSP, maspct = sp.MaChiTietSP });
                    }
                }
                else
                {
                    SanPham SanPham = _product.GetSanpham(sp.MaSP);
                    Mau mau = _mau.GetMau(sp.MaMau);

                shoppingCart.Add(new ShoppingCartItem()
                    {
                        sanphamct = sp,
                        Name = SanPham.TenSanPham,
                        TenMau = mau.TenMau,
                        Quantity = 1,
                        tonkho = slton,
                        GiaGoc = (decimal)SanPham.GiaSanPham,
                        PhanTramGiam = kmRepo.GetKmProductToday(SanPham),
                        Size = tenSize,
                        Maspsize = _tkho.GetMaspsize(sp.MaChiTietSP, tenSize)
                    });
                }
                       
            Session["Cart"] = shoppingCart;
            return View(shoppingCart);
        }

            public ActionResult IncreaseOne(int Masp, string size)
            {
            
            string cartJson = Session["Cart"] as string;

            
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

