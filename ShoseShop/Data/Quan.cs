using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class Quan
    {
        public int MaQuan { get; set; }
    
     public string TenQuan { get; set; } // Tên quận

        [Index]
        public int MaTinh { get; set; } // Mã tỉnh liên kết với quận
        public virtual Tinh MaTinhNavigation { get; set; } // Tỉnh liên kết với quận
        
        public virtual ICollection<Phuong> Phuongs { get; set; } = new List<Phuong>(); // Danh sách các phường thuộc quận

        // Thêm các thuộc tính khác nếu cần thiết

    }
}