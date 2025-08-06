using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoseShop.ViewModel
{
    public class AddressNoteBookViewModel
    {
        public string HoTen { get; set; }
        public string Sdt { get; set; }
        public List<Tinh> tinhList { get; set; }
        public List<Quan> quanList { get; set; }
        public List<Phuong> phuongList { get; set; }
    
}
}