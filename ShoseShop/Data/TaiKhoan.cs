using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class TaiKhoan
    {
        public int MaKhachHang { get; set; } // Mã tài khoản
        public virtual KhachHang MaKHNavigation { get; set; } // Size liên kết với sản phẩm

        public int MaNhanVien { get; set; } // Mã tài khoản
        public virtual NhanVien  MaNVNavigation { get; set; } // Size liên kết với sản phẩm
       public  string Email { get; set; }
        public int LoaiTK { get; set; }
    
        public string MatKhau { get; set; } // Mật khẩu của tài khoản

    }
}