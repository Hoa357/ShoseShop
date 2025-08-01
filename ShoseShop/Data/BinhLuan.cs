using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class BinhLuan
    {
        public int MaBinhLuan { get; set; } // Mã bình luận

        public string NoiDung { get; set; } // Nội dung bình luận

        public int MaSP { get; set; } // Mã sản phẩm
        
        public int MaKH { get; set; } // Mã khách hàng
        public virtual SanPham MaSPNavigation { get; set; }

        public int Rating { get; set; }
        public DateTime NgayBinhLuan { get; set; } // Ngày bình luận

        public virtual KhachHang MaKHNavigation { get; set; }

    }
}