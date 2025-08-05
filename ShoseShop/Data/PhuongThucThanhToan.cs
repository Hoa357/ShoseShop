using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class PhuongThucThanhToan
    {
        public int MaPTTT { get; set; }
        public virtual ICollection<PhieuMua> PhieuMuas { get; set; } = new List<PhieuMua>(); 
        

        public string TenPTTT { get; set; } // Tên phương thức thanh toán (ví dụ: "Tiền mặt", "Chuyển khoản", "Thẻ tín dụng", ...)
    }
}