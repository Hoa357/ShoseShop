using ShoseShop.InterfaceRepositories;
using ShoseShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace ShoseShop.Repositories
{
    public class VoucherRepo : IVoucher
    {


        private readonly ShoesContext _db; 
        private object _phieuMuaRepository;

        public VoucherRepo(ShoesContext db)
        {
            _db = db;
          
        }

        public List<Voucher> getAllVoucherToday()
        {
            System.DateTime today = System.DateTime.Now.Date;
            List<Voucher> AllVoucherToday = _db.Vouchers.Where(x => x.SoLuong > 0 && today >= x.NgayBatDau.Date && today <= x.NgayKetThuc).ToList();
            return AllVoucherToday;
        }

        public Voucher GetVoucherByCode(string id)
        {
            return _db.Vouchers.FirstOrDefault(x => x.MaVoucher == id);
        }

     
    }
}
