using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class SanPham
    {
        public  int MaSanPham { get; set; }  
        public string TenSanPham { get; set; }
        public int Maloai { get; set; }

        public double GiaSanPham { get; set; } // Giá sản phẩm
        public string MoTa { get; set; } // Mô tả sản phẩm


        public virtual Loai MaloaiNavigation { get; set; }

        public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; } = new List<ChiTietKhuyenMai>();
        public virtual ICollection<BinhLuan> Binhluans { get; set; } = new List<BinhLuan>();

        public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; } = new List<ChiTietSanPham>();


    }
}