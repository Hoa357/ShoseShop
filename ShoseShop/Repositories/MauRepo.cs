using ShoseShop.InterfaceRepositories;
using ShoseShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace ShoesStore.Repositories
{
	public class MauRepo : IMau
	{
        private readonly ShoesContext _db;
        

        public MauRepo(ShoesContext db)
        {
            _db = db;

        }

        public Mau GetMau(int mamau)
		{
			Mau m = _db.Maus.FirstOrDefault(x => x.MaMau == mamau);
			return m;
		}

		public List<Mau> GetMauList()
		{
			return _db.Maus.ToList();
		}

      
    }
}
