using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewModel
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TenMau { get; set; }
        public ChiTietSanPham sanphamct { get; set; }
        public string Size { get; set; }
        public int Maspsize { get; set; }
        public int Quantity { get; set; }
        public decimal GiaGoc { get; set; }
        public int PhanTramGiam { get; set; }
        public int tonkho { get; set; }
    }
}