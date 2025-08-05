using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class Mau
    {
        public int MaMau { get; set; } // Mã màu
        public string TenMau { get; set; } // Tên màu
        public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; } = new List<ChiTietSanPham>();

        // Thêm các thuộc tính khác nếu cần thiết
    }
}