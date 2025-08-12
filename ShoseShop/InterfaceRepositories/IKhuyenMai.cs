using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface IKhuyenMai
    {
        DateTime getNgayktKmToday();
        List<KhuyenMai> GetAllKhuyenMaiToday(string searchString, int maMau, int? sortGia, decimal? minPrice, decimal? maxPrice, int Phantramgiam);
        int GetKmProductToday(SanPham sp);
    
}
}
