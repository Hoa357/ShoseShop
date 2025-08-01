using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Routing.Constraints;

namespace ShoseShop.Data
{
    public class Voucher
    {
        public string MaVoucher { get; set; } // Mã voucher
        public int SoLuong { get; set; } // Tên voucher

        public Double GiaToiThieu { get; set; } // Giá trị tối thiểu để áp dụng voucher
        public Double GiaToiDa { get; set; } // Giá trị giảm giá của voucher
        
        public DateTime NgayBatDau { get; set; } // Ngày bắt đầu áp dụng voucher
        public DateTime NgayKetThuc { get; set; } // Ngày kết thúc áp dụng voucher

        public virtual ICollection<PhieuMua> Phieumuas { get; set; } = new List<PhieuMua>();
    }
}