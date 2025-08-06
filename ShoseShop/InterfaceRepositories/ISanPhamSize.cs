using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    internal interface ISanPhamSize
    {

        SanPhamSize GetSLTon(int masp, int masize);
        void MinusSanPhamSize(PhieuMuaViewModel pm);
        int GetMaspsize(int masp, string tensize);
    }
}
