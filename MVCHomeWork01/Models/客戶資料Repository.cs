using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVCHomeWork01.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(a => !a.是否已刪除);
        }

        public 客戶資料 Find(int id)
        {
            return All().FirstOrDefault(a => a.Id == id);
        }

        public void AddData(客戶資料 客戶資料)
        {
            Add(客戶資料);
            UnitOfWork.Commit();
        }

        public void UpdateData(客戶資料 客戶資料)
        {
            UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
            UnitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var client = Find(id);
            if (client != null)
            {
                client.是否已刪除 = true;

                var repoContact = RepositoryHelper.Get客戶聯絡人Repository(UnitOfWork);
                repoContact.DeleteByDeleteClient(client.客戶聯絡人);

                var repoBank = RepositoryHelper.Get客戶銀行資訊Repository(UnitOfWork);
                repoBank.DeleteByDeleteClient(client.客戶銀行資訊);

                UnitOfWork.Commit();
            }
        }

        public SelectList GetSelect(int? id)
        {
            return id.HasValue
                ? new SelectList(All(), "Id", "客戶名稱", id.Value)
                : new SelectList(All(), "Id", "客戶名稱");
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}