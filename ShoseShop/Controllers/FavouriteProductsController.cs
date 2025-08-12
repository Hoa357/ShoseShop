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
    public class FavouriteProductsController : Controller
    {
        private readonly     IChiTietSanPham _sanphamct;
            ISanPham _product;
            ISize _size;
            ISanPhamSize _tkho;
            IMau _mau; IKhuyenMai kmRepo;
            public FavouriteProductsController(IChiTietSanPham productDetail, ISanPham product, ISize sz, ISanPhamSize tkho, IMau mau, IKhuyenMai kmRepo)
            {
                _sanphamct = productDetail;
                _product = product;
                _size = sz;
                _tkho = tkho;
                _mau = mau;
                this.kmRepo = kmRepo;
            }

            
            public FavouriteProductsController()
            {
                var db = new ShoesContext(); // Tạo DbContext
                _sanphamct = new ChiTietSanphamRepo(db); 
                _product = new SanphamRepo(db);
                _size = new SizeRepo(db);
                _tkho = new SanphamSizeRepo(db);
                _mau = new MauRepo(db);
                this.kmRepo = new KhuyenMaiRepo(db);
            }

        public ActionResult ViewFavouriteProducts()
            {
         
            var favouriteItems = new List<FavouriteProductsItem>();

            string favouriteJson = Session["Favourite"] as string;

            // 3. Kiểm tra xem có lấy được chuỗi JSON nào không.
            if (!string.IsNullOrEmpty(favouriteJson))
            {
                
                favouriteItems = JsonConvert.DeserializeObject<List<FavouriteProductsItem>>(favouriteJson);
            }

           
            return View(favouriteItems);
            }

            public ActionResult AddFavouriteProducts(int id)
            {
            var favouriteItems = new List<FavouriteProductsItem>();

            string favouriteJson = Session["Favourite"] as string;

            // 3. Kiểm tra xem có lấy được chuỗi JSON nào không.
            if (!string.IsNullOrEmpty(favouriteJson))
            {

                favouriteItems = JsonConvert.DeserializeObject<List<FavouriteProductsItem>>(favouriteJson);
            }

            var existingFavouriteItem = favouriteItems.FirstOrDefault(item => item.Id == id);

                if (existingFavouriteItem == null)
                {
                    favouriteItems.Add(_sanphamct.GetFavProById(id));
                }

            string jsonToSave = JsonConvert.SerializeObject(favouriteItems);

           
            Session["Favourite"] = jsonToSave;
            return RedirectToAction("ViewFavouriteProducts");
        
             }
            public ActionResult RemoveFavouriteProduct(int id)
            {
            var favouriteItems = new List<FavouriteProductsItem>();

            string favouriteJson = Session["Favourite"] as string;

            // 3. Kiểm tra xem có lấy được chuỗi JSON nào không.
            if (!string.IsNullOrEmpty(favouriteJson))
            {

                favouriteItems = JsonConvert.DeserializeObject<List<FavouriteProductsItem>>(favouriteJson);
            }

            var itemToRemove = favouriteItems.FirstOrDefault(item => item.Id == id);

                if (itemToRemove != null)
                {
                    favouriteItems.Remove(itemToRemove);
                string jsonToSave = JsonConvert.SerializeObject(favouriteItems);

                
                Session["Favourite"] = jsonToSave;

                TempData["Message"] = "Sản phẩm đã được xóa thành công khỏi danh sách yêu thích của bạn.";
                }
                else
                {
                    TempData["Message"] = "Không tìm thấy sản phẩm trong mục yêu thích của bạn.";
                }

                return RedirectToAction(nameof(ViewFavouriteProducts));
            }

        }
    }

