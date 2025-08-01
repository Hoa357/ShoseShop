using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class Phuong
    {
        public int MaPhuong { get; set; } // Mã phường
        public string TenPhuong { get; set; } // Tên phường
        public int MaQuan { get; set; } // Mã quận liên kết với phường
        public virtual Quan MaQuanNavigation { get; set; } // Quận liên kết với phường
     
    }
}