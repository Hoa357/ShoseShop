using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace ShoseShop.Repositories
{
    public class SizeRepo : ISize
    {
        ShoesContext __db;
        public SizeRepo(ShoesContext _db)
        {
            __db = _db;
        }


        public Size GetSize(int masize)
        {
            Size sz = __db.Sizes.FirstOrDefault(x => x.MaSize == masize);
            return sz;
        }

        public Size GetSizeByName(string name)
        {
            Size sz = __db.Sizes.FirstOrDefault(x => x.TenSize == name);
            return sz;
        }
    }
}