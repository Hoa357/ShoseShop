using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class Size
    {
        public int MaSize { get; set; } // Mã size
        public string TenSize { get; set; } // Tên size (ví dụ: "S", "M", "L", "XL", "42", "43", ...)
        
        public virtual ICollection<SanPhamSize> SanPhamSizes { get; set; } = new List<SanPhamSize>();

        // Thêm các thuộc tính khác nếu cần thiết
    }
}