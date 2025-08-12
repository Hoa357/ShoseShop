using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;
namespace ShoseShop.Repositories
{
    public class PhieuMuaRepo : IPhieuMua
    {
        ShoesContext _db;
        public PhieuMuaRepo(ShoesContext _db)
        {
            this._db = _db;
        }

        public int AddPhieuMua(PhieuMuaViewModel phieuMua)
        {
            // Kiểm tra các trường bắt buộc
            if (!phieuMua.maTinh.HasValue || !phieuMua.maQuan.HasValue || !phieuMua.maPhuong.HasValue)
            {
                throw new ArgumentException("Vui lòng chọn đầy đủ Tỉnh, Quận, Phường.");
            }

            // Lấy thông tin tỉnh, quận, phường
            var tinh = _db.Tinhs.FirstOrDefault(x => x.Matinh == phieuMua.maTinh);
            var quan = _db.Quans.FirstOrDefault(x => x.MaQuan == phieuMua.maQuan);
            var phuong = _db.Phuongs.FirstOrDefault(x => x.MaPhuong == phieuMua.maPhuong);

            // Kiểm tra null
            if (tinh == null || quan == null || phuong == null)
            {
                throw new ArgumentException("Thông tin Tỉnh, Quận hoặc Phường không hợp lệ.");
            }

            string tentinh = tinh.Tentinh;
            string tenquan = quan.TenQuan;
            string tenphuong = phuong.TenPhuong;

            // Xử lý khách hàng
            if (phieuMua.khInfo != null)
            {
                KhachHang kh = _db.Khachhangs.FirstOrDefault(x => x.MaKhachHang == phieuMua.khInfo.MaKhachHang);
                if (kh != null)
                {
                    kh.TongXu -= phieuMua.coinApply;
                    _db.SaveChanges();
                }
            }

            // Xử lý voucher
            if (phieuMua.Choosenvoucher?.MaVoucher != null)
            {
                Voucher vc = _db.Vouchers.FirstOrDefault(x => x.MaVoucher == phieuMua.Choosenvoucher.MaVoucher);
                if (vc != null)
                {
                    vc.SoLuong -= 1;
                    _db.SaveChanges();
                }
            }

            // Tạo địa chỉ
            string Diachi = $"{phieuMua.Diachi}, {tentinh}, {tenquan}, {tenphuong}";

            // Tạo phiếu mua
            PhieuMua newpm = new PhieuMua()
            {
                GhiChu = phieuMua.GhiChu,
                MaKH = phieuMua.khInfo?.MaKhachHang,
                NgayMua = DateTime.Now,
                TinhTrang = "Pending",
                MaPTTT = phieuMua.Mapttt,
                MaPTTTNavigation = _db.Phuongthucthanhtoans.Find(phieuMua.Mapttt),
                TongTien = phieuMua.totalCost,
                TenNguoiNhan = phieuMua.HoTen,
                SoDienThoaiNguoiNhan = phieuMua.Sdt,
                EmailNguoiNhan = phieuMua.Email,
                DiaChiNguoiNhan = Diachi,
                MaVoucher = phieuMua.Choosenvoucher?.MaVoucher
            };

            _db.Phieumuas.Add(newpm);
            _db.SaveChanges();

            // Tạo chi tiết phiếu mua
            List<ChiTietPhieuMua> ctpmList = new List<ChiTietPhieuMua>();
            foreach (ShoppingCartItem cartItem in phieuMua.listcartItem)
            {
                ctpmList.Add(new ChiTietPhieuMua()
                {
                    MaPhieuMua = newpm.MaPhieuMua,
                    MaSanPhamSize = cartItem.Maspsize,
                    SoLuong = cartItem.Quantity,
                    DonGia = cartItem.PhanTramGiam > 1 ? (cartItem.GiaGoc - cartItem.GiaGoc * cartItem.PhanTramGiam / 100) : cartItem.GiaGoc,
                });
            }

            _db.Chitietphieumuas.AddRange(ctpmList);
            _db.SaveChanges();
            return newpm.MaPhieuMua;
        }
        public List<PhieuMua> GetOrderHistoryByEmail(string email)
        {
            var khachhang = _db.Khachhangs.FirstOrDefault(kh => kh.Email == email);
            if (khachhang == null) return new List<PhieuMua>();
            return _db.Phieumuas.Where(pm => pm.MaKH == khachhang.MaKhachHang)
                .Include(x => x.MaVoucherNavigation)
                .Include(p => p.ChiTietPhieuMuas.Select(c => c.MaSPSizeNavigation.MaSPCTNavigation.MaSPNavigation))
                .Include(p => p.ChiTietPhieuMuas.Select(c => c.MaSPSizeNavigation.MaSizeNavigation))
                .Include(p => p.ChiTietPhieuMuas.Select(c => c.MaSPSizeNavigation.MaSPCTNavigation.MaMauNavigation))
                .OrderByDescending(x => x.MaPhieuMua)
                .ToList();
        }
        public PhieuMua GetOrderById(int id)
        {
            return _db.Phieumuas
                .Include(p => p.ChiTietPhieuMuas.Select(c => c.MaSPSizeNavigation.MaSPCTNavigation.MaSPNavigation))
                .Include(p => p.ChiTietPhieuMuas.Select(c => c.MaSPSizeNavigation.MaSizeNavigation))
                .Include(p => p.ChiTietPhieuMuas.Select(c => c.MaSPSizeNavigation.MaSPCTNavigation.MaMauNavigation))
                .FirstOrDefault(p => p.MaPhieuMua == id);
        }
    }
}
