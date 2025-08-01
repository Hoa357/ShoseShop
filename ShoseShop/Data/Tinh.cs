using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.Data
{
    public class Tinh
    {
        public int Matinh { get; set; }

        public string Tentinh { get; set; } 

        public virtual ICollection<Quan> Quans { get; set; } = new List<Quan>();
    
}
}