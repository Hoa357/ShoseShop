using ShoseShop.Data;
using ShoseShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
     public interface IBinhLuan
    {
        void AddBinhLuan(BinhLuan bl);
        CommentViewModel GetBlList(int masp, int page = 1);
    }
}
