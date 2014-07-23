using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace MVCHomeWork02.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(a => !a.是否已刪除);
        }

        public List<客戶聯絡人> GetAllData()
        {
            return All().Include(a => a.客戶資料).ToList();
        }

        public 客戶聯絡人 Find(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }

        public void AddData(客戶聯絡人 客戶聯絡人)
        {
            Add(客戶聯絡人);
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

        public void DeleteByDeleteClient(IEnumerable<客戶聯絡人> contacts)
        {
            foreach (var contact in contacts.ToList())
            {
                Delete(contact.Id);
            }
        }

        public IEnumerable<客戶聯絡人> GetAllDataByClientId(int id)
        {
            return this.All().Where(a => a.客戶Id == id);
        }

        public bool CheckMailDuplicate(int id, string email)
        {
            var hasDuplicate = this.Where(a => a.Id != id && a.Email == email).Any();

            return hasDuplicate;
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}