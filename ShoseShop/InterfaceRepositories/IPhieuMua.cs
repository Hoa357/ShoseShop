using ShoseShop.Data;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface IPhieuMua
    {
        int AddPhieuMua(PhieuMuaViewModel phieuMua);
        List<PhieuMua> GetOrderHistoryByEmail(string email);
        PhieuMua GetOrderById(int id);
    }
}
