using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace MVCHomeWork01.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(a => !a.是否已刪除);
        }

        public List<客戶銀行資訊> GetAllData()
        {
            return All().Include(a => a.客戶資料).ToList();
        }

        public 客戶銀行資訊 Find(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }

        public void AddData(客戶銀行資訊 客戶銀行資訊)
        {
            Add(客戶銀行資訊);
            UnitOfWork.Commit();
        }

        public void UpdateData(客戶銀行資訊 客戶銀行資訊)
        {
            UnitOfWork.Context.Entry(客戶銀行資訊).State = EntityState.Modified;
            UnitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var data = Find(id);
            if (data != null)
            {
                data.是否已刪除 = true;
                UnitOfWork.Commit();
            }
        }

        public void DeleteByDeleteClient(IEnumerable<客戶銀行資訊> banks)
        {
            foreach (var bank in banks.ToList())
            {
                Delete(bank.Id);
            }
        }
    }

    public interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {

    }
}