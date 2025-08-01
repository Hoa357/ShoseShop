using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class KhuyenMai
    {
        public int MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int PhanTramGiam { get; set  ; } // Mức giảm giá, có thể là phần trăm hoặc số tiền cụ thể
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKMs { get; set; } = new List<ChiTietKhuyenMai>();
    }
}