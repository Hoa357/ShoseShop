using ShoseShop.Data;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface ISanPham
    {
        SanPham GetSanpham(int masp);
        List<SanphamViewModel> GetSanPhamView(int maMau, int? sortGia, string searchString, decimal? minPrice, decimal? maxPrice, int maLoai);
    
}
}
