using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class ChiTietSanPham
    {
 
        public int MaChiTietSP { get; set; } // Mã chi tiết sản phẩm

        public int MaSP { get; set; } // Mã sản phẩm liên kết với chi tiết sản phẩm

        [Index]
        public int MaMau { get; set; } // Mã màu của sản phẩm
        public virtual SanPham MaSPNavigation { get; set; } // Sản phẩm liên kết với chi tiết khuyến mãi

        public virtual Mau MaMauNavigation { get; set; } // Sản phẩm liên kết với chi tiết khuyến mãi

        public virtual ICollection<SanPhamSize> Sanphamsizes { get; set; } = new List<SanPhamSize>();

        public string AnhDaiDien { get; set; } // Mã loại
        public string AnhMatTren { get; set; } // Mã màu
        public string AnhDeGiay { get; set; } // Mã size
        public string Video  { get; set; } // Giá bán
        public TrangThaiEnum TrangThai { get; set; } // Số lượng tồn kho


        public enum TrangThaiEnum
        {
            DangBan = 1,
            NgungBan = 2,
            Hot = 3,
            New = 4
        }


        // Thêm các thuộc tính khác nếu cần thiết
    }
}