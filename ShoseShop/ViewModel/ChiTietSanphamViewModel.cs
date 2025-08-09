using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewModel
{
    public class ChiTietSanphamViewModel
    {
        public SanPham sanpham { get; set; }
        public List<ChiTietKhuyenMai> sanphamct { get; set; }
        public List<string> tenSize { get; set; }
        public List<int> slton { get; set; }
    }

}