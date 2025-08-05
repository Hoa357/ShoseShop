using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class ChiTietKhuyenMai
    {
        public int MaKM { get; set; }
        public int MaSP { get; set; }
        
        public virtual KhuyenMai MaKMNavigation { get; set; } // Sản phẩm liên kết với chi tiết khuyến mãi
        public virtual SanPham MaSPNavigation { get; set; } // Sản phẩm liên kết với chi tiết khuyến mãi
       
        public int SoLuong { get; set; } // Số lượng sản phẩm trong khuyến mãi
    }
}