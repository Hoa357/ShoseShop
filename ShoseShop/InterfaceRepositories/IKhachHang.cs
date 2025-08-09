using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface IKhachHang
    {
                
        KhachHang GetCurrentKh(string emailkh);
        void UpdateKh(KhachHang khachhang);
    

}
}
