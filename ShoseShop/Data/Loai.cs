using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class Loai
    {
        public int MaLoai { get; set; }
        public string TeLoai { get; set; }

        public virtual ICollection<ChiTietSanPham> ChiTietSP { get; set; } = new List<ChiTietSanPham>();


        
}
}