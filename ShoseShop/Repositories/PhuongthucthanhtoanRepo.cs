using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace ShoseShop.Repositories
{
    public class PhuongthucthanhtoanRepo : IPhuongthucthanhtoan
    {
        ShoesContext _db;
        public PhuongthucthanhtoanRepo(ShoesContext data)
        {
            this._db = data;
        }
        public List<PhuongThucThanhToan> GetAllPttt()
        {
            List<PhuongThucThanhToan> ptttList =_db.Phuongthucthanhtoans.ToList();
            return ptttList;
        }
    }
}
