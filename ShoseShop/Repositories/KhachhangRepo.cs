using ShoseShop.InterfaceRepositories;
using ShoseShop.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Migrations;
namespace ShoesStore.Repositories
{
    public class KhachhangRepo : IKhachHang
    {
        ShoesContext _db;
        public KhachhangRepo(ShoesContext _db)
        {
            this._db = _db;
        }

        public KhachHang GetCurrentKh(string emailkh)
        {

            KhachHang kh = _db.Khachhangs.FirstOrDefault(x => x.Email == emailkh);
            return kh;
        }

        public void UpdateKh(KhachHang khachhang)
        {
            _db.Khachhangs.AddOrUpdate(khachhang);
            _db.SaveChanges();
        }
    }
    }
