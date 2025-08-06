using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewModel
{
    public class RegisterViewModel
    {
        public KhachHang Khachhang { get; set; } = new KhachHang();
        public TaiKhoan Taikhoan { get; set; } = new TaiKhoan();

    }
}