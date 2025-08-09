using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class NhanVien
    {
        [Key]
       public int MaNV { get; set; } // Mã nhân viên

        public virtual PhieuMua MaPMNavigation { get; set; } // Size liên kết với sản phẩm

        public virtual TaiKhoan EmailNavigation { get; set; } // Size liên kết với sản phẩm


        public string TenNV { get; set; } // Tên nhân viên

        [Index]
        [MaxLength(255)] // BẮT BUỘC nếu dùng Index trên string
        public string Email { get; set; } // Email của nhân viên
        public string SoDienThoai { get; set; } // Số điện thoại của nhân viên
        public DateTime NgaySinh { get; set; } // Ngày sinh của nhân viên
       
        public bool GioiTinh { get; set; } // Giới tính của nhân viên (true: Nam, false: Nữ)
        public string DiaChi { get; set; } // Địa chỉ của nhân viên
        public string ChucVu { get; set; } // Chức vụ của nhân viên (ví dụ: "Quản lý", "Nhân viên bán hàng", ...)
        public DateTime NgayVaoLam { get; set; } // Ngày vào làm của nhân viên
        public bool TrangThai { get; set; } // Trạng thái làm việc (true: đang làm, false: đã nghỉ việc)

        public virtual ICollection<PhieuMua> PhieuMuas { get; set; } = new List<PhieuMua>();
    
}
}