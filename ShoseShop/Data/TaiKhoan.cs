using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class TaiKhoan
    {
       
        public virtual KhachHang KhachHangs { get; set; } // Size liên kết với sản phẩm

      
        public virtual NhanVien NhanViens { get; set; } // Size liên kết với sản phẩm
       public  string Email { get; set; }
        public int LoaiTK { get; set; }
    
        public string MatKhau { get; set; } // Mật khẩu của tài khoản

    }
}