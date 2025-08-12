using PagedList;
using ShoseShop.Data;
using ShoseShop.InterfaceRepositories;
using ShoseShop.ViewModel;
using System.Collections.Generic;
using System.Linq;


namespace ShoseShop.Repositories
{
    public class AddressNoteBookRepo : IAddressNoteBook
    {

        private readonly ShoesContext _db;
       

        public AddressNoteBookRepo(ShoesContext db)
        {
            _db = db;

        }

        public List<Tinh> GetTinhList()
        {
            return _db.Tinhs.ToList();
        }
        public List<Quan> GetQuanList(int provinces)
        {
            return _db.Quans.Where(x=>x.MaTinh == provinces).ToList();
        }
        public List<Phuong> GetPhuongList(int districts)
        {
            return _db.Phuongs.Where(x => x.MaQuan == districts).ToList();
        }

        public List<SoDiaChi> GetAllAddressNote()
        {
            return _db.SoDiaChis.ToList();
        }

        public List<SoDiaChi> GetAllAddressNoteByMaKH(int maKH)
        {
            return _db.SoDiaChis.Where(x => x.MaKH == maKH).ToList();
        }
        public List<SoDiaChi> GetAllAddressNoteById(int masdc)
        {
            return _db.SoDiaChis.Where(x => x.MaSoDiaChi == masdc).ToList();
        } 

        public int GetMaTinh(string TenTinh)
        {
             return _db.Tinhs.FirstOrDefault(x=>x.Tentinh == TenTinh).Matinh;
        }
        public int GetMaQuan(string TenQuan)
        {
            return _db.Quans.FirstOrDefault(x => x.TenQuan == TenQuan).MaQuan;
        }
        public int GetMaPhuong(string TenPhuong)
        {
            return _db.Phuongs.FirstOrDefault(x => x.TenPhuong == TenPhuong).MaPhuong;
        }
        public void AddAddressNote(int proviceId, int districtId, int wardId,string address,int makh,string tennguoinhan,string sdt)
        {
            Tinh province = _db.Tinhs.Find(proviceId);
            Quan district = _db.Quans.Find(districtId);
            Phuong ward = _db.Phuongs.Find(wardId);

            string finalAddress = address + ", "+province.Tentinh + ", "+district.TenQuan + ", "+ward.TenPhuong;

            SoDiaChi sdc = new SoDiaChi
            {
                TenNguoiNhan = tennguoinhan,
                SoDTNguoiNhan = sdt,
                MaKH = makh,
                DiaChi = finalAddress
            };

            _db.SoDiaChis.Add(sdc);
            _db.SaveChanges();
        }
        

        public SoDiaChi GetSoDiaChi(int maSoDiaChi)
        {
            return _db.SoDiaChis.FirstOrDefault(x => x.MaSoDiaChi == maSoDiaChi);
        }

        public void UpdateSDC(int masdc, string hoten, string sdt, string diachi, int matinh, int maquan, int maphuong)
        {
            SoDiaChi sdc = _db.SoDiaChis.FirstOrDefault(x => x.MaSoDiaChi == masdc);
            string tentinh = _db.Tinhs.FirstOrDefault(x => x.Matinh == matinh).Tentinh;
            string tenquan = _db.Quans.FirstOrDefault(x => x.MaQuan == maquan).TenQuan;
            string tenphuong = _db.Phuongs.FirstOrDefault(x => x.MaPhuong == maphuong).TenPhuong;
            string diachiFinal = diachi +", "+tentinh+", "+tenquan+", "+tenphuong;

            sdc.TenNguoiNhan = hoten;
            sdc.SoDTNguoiNhan = sdt;
            sdc.DiaChi = diachiFinal;
          
            _db.SaveChanges();
        }

        public void DeleteSDC(int masdc)
        {
            SoDiaChi sdc = _db.SoDiaChis.FirstOrDefault(x => x.MaSoDiaChi == masdc);
            _db.SoDiaChis.Remove(sdc);
            _db.SaveChanges();
        }

      
    }
}
