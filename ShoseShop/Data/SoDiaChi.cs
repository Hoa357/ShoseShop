using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class SoDiaChi
    {
        public virtual KhachHang MaKHNavigation { get; set; }
        public int MaKH { get; set; } // Mã khách hàng
        public int MaSoDiaChi { get; set; } // Mã số địa chỉ

        public string TenNguoiNhan { get; set; } // Tên địa chỉ
    
        public string SoDTNguoiNhan { get; set; } // Số điện thoại người nhận
        public string DiaChi { get; set; } // Địa chỉ cụ thể
    }
}