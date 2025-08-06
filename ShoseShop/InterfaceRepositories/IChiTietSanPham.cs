using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.InterfaceRepositories
{
    public class IChiTietSanPham
    {
        ChiTietSanPham Getsanphamct(int masp);
        SanphamViewModel HienThiSanpham(int madongsanpham, int masp);
        List<SanPhamHomeViewModel> HomeSanPham(int trangthai);
        FavouriteProductsItem GetFavProById(int id);
    }
}