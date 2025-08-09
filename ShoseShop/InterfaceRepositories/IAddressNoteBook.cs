using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface IAddressNoteBook
    {
         List<Tinh> GetTinhs();
         List<Quan> GetQuanList(int provinces);
         List<Phuong> GetPhuongList(int districts);
         List<SoDiaChi> GetAllAddressNote();

         List<SoDiaChi> GetAllAddressNoteByMaKH(int maKH);

         List<SoDiaChi> GetAllAddressNoteById(int masdc);

        void AddAddressNote(int proviceId, int districtId, int wardId, string address, int makh, string tennguoinhan, string sdt);
         SoDiaChi GetSodiachi(int masodiachi);
         int GetMaTinh(string TenTinh);
         int GetMaQuan(string TenQuan);
         int GetMaPhuong(string TenPhuong);
         void UpdateSDC(int masdc, string hoten, string sdt, string diachi, int matinh, int maquan, int maphuong);
         void DeleteSDC(int masdc);
    }

}
