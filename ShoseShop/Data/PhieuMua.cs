using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace ShoseShop.Data
{
    public class PhieuMua
    {
        public int MaPhieuMua { get; set; } // Mã phiếu mua hàng
        public DateTime NgayMua { get; set; } // Ngày mua hàng
       
        public int? MaKH { get; set; } // Mã khách hàng (có thể null nếu không có khách hàng liên kết)
        public virtual KhachHang MaKHNavigation { get; set; } // Mã khách hàng liên kết với phiếu mua hàng

        [Index]
        public int? MaNV { get; set; } // Mã nhân viên (có thể null nếu không có nhân viên liên kết)
        public virtual NhanVien MaNVNavigation { get; set; } // Mã khách hàng liên kết với phiếu mua hàng

        public string MaVoucher { get; set; } // Mã voucher (có thể null nếu không có voucher liên kết)
        public virtual Voucher MaVoucherNavigation { get; set; } // Size liên kết với sản phẩm

        public virtual PhuongThucThanhToan MaPTTTNavigation { get; set; } // Phương thức thanh toán liên kết với phiếu mua hàng

        [Index]
        public int MaPTTT { get; set; } // Mã phương thức thanh toán (có thể là mã của một phương thức thanh toán cụ thể)
        public virtual PhieuMua MaPMNavigation { get; set; } // Size liên kết với sản phẩm

        public string TinhTrang { get; set; } // Tình trạng của phiếu mua hàng (ví dụ: "Đã thanh toán", "Chưa thanh toán", "Đã giao hàng", "Đang xử lý", ...)
       
        public string GhiChu { get; set; } // Ghi chú cho phiếu mua hàng (nếu có)

        public string LyDoHuyDon { get; set; } // Lý do hủy đơn hàng (nếu có, có thể null nếu không hủy)

        public decimal TongTien { get; set; } // Tổng tiền của phiếu mua hàng
        
        public DateTime? NgayHuyDon { get; set; } // Ngày hủy đơn hàng (nếu có, có thể null nếu không hủy)

        public string DiaChiNguoiNhan { get; set; } // Địa chỉ người nhận hàng
        public string EmailNguoiNhan { get; set; } // Email người nhận hàng

        public string SoDienThoaiNguoiNhan { get; set; } // Số điện thoại người nhận hàng

        [DefaultValue("(N'')")]
        public string TenNguoiNhan { get; set; } // Tên người nhận hàng
        public virtual ICollection<ChiTietPhieuMua> ChiTietPhieuMuas { get; set; } = new List<ChiTietPhieuMua>(); // Chi tiết các sản phẩm trong phiếu mua hàng
    }
}