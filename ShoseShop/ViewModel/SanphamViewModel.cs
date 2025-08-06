using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewModel
{
    public class SanphamViewModel
    {
        public SanPham sanpham { get; set; }
        public List<ChiTietSanPham> sanphamcts { get; set; }
        public List<string> tenSize { get; set; }
        public List<int> slton { get; set; }

    
}
}