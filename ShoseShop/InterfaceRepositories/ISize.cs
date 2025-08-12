using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface ISize
    {
      
        Size GetSize(int masize);
        Size GetSizeByName(string name);
    }
}
