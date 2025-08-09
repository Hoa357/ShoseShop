using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class KhachHang
    {
        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string Email { get; set; }

        public string Phone { get ; set; }
        public bool? GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public decimal TongXu { get; set; }

        public virtual ICollection<SoDiaChi> MaSoDCs { get; set; } = new List<SoDiaChi>();

        public virtual ICollection<BinhLuan> Binhluans { get; set; } = new List<BinhLuan>();

       
        public virtual TaiKhoan EmailNavigation { get; set; }

        public virtual ICollection<PhieuMua> Phieumuas { get; set; } = new List<PhieuMua>();

        public virtual ICollection<SoDiaChi> Sodiachis { get; set; } = new List<SoDiaChi>();
    }
}
