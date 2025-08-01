using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class SoXu
    {
        public int Masoxu { get; set; }

        public int Makh { get; set; }

        public decimal Tongxu { get; set; }

        public virtual KhachHang MakhNavigation { get; set; } 
    
}
}