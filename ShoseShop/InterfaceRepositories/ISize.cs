using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    internal interface ISize
    {
      
        Size GetSize(int masize);
        Size GetSizeByName(string name);
    }
}
