using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class SanPhamSize
    {
        public int MaSanPhamSize { get; set; } // Mã sản phẩm và size

        public int Masp { get; set; }

        public int Masize { get; set; }
        public ICollection<ChiTietPhieuMua> MaCTPMs { get; set; } // Sản phẩm liên kết với size
        public virtual Size MaSizeNavigation { get; set; } // Size liên kết với sản phẩm
        public int SoLuongTonKho { get; set; } // Số lượng tồn kho cho sản phẩm và size cụ thể
        public virtual SanPham MaspNavigation { get; set; } 
    }
}