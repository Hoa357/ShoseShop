using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace ShoseShop.Data
{
    public class SanPhamSize
    {
        public int MaSanPhamSize { get; set; } // Mã sản phẩm và size

        [Index]
        public int Maspct { get; set; }

        [Index]
        public int Masize { get; set; }
        public ICollection<ChiTietPhieuMua> ChiTietPhieuMuas { get; set; } // Sản phẩm liên kết với size
        public virtual Size MaSizeNavigation { get; set; } // Size liên kết với sản phẩm
        public int SoLuongTonKho { get; set; } // Số lượng tồn kho cho sản phẩm và size cụ thể
        public virtual ChiTietSanPham MaSPCTNavigation { get; set; } 



    }
}