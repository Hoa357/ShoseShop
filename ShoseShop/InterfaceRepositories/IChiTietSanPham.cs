using ShoseShop.Data;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface IChiTietSanPham
    {
        ChiTietSanPham Getsanphamct(int maspct);
        ChiTietSanphamViewModel HienThiSanpham(int maSanPham, int maspct);
        List<SanPhamHomeViewModel> HomeSanPham(int trangthai);
        FavouriteProductsItem GetFavProById(int id);
    
}
}
