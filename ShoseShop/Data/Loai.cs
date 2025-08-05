using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class Loai
    {
        public int MaLoai { get; set; }

        [MaxLength(255)]
        [Index]
        public string TenLoai { get; set; }

        public int MaSanPham { get; set; } // Mã sản phẩm liên kết với loại
        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();


        
}
}