using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class ChiTietPhieuMua
    {
        public int MaPhieuMua { get; set; } // Mã phiếu mua hàng
        public int MaSanPhamSize { get; set; } // Mã sản phẩm và size

        public virtual SanPhamSize MaSPSizeNavigation { get; set; } // Mã sản phẩm và size liên kết với chi tiết phiếu mua hàng
        public virtual PhieuMua MaPMNavigation { get; set; } // Mã phiếu mua hàng liên kết với chi tiết phiếu mua hàng
        public int SoLuong { get; set; } // Số lượng sản phẩm trong phiếu mua hàng
        public decimal DonGia { get; set; }

    }
}