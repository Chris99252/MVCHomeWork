using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCHomeWork02.Helper;
using MVCHomeWork02.Models;
using WebGrease.Css.Extensions;

namespace MVCHomeWork02.Controllers
{
    public class ClientController : BaseController
    {
        private readonly 客戶資料Repository repoClient;
        private readonly ReportRepository repoReport;
        private readonly 客戶聯絡人Repository repoContact;
        private readonly 客戶銀行資訊Repository repoBank;

        public ClientController()
        {
            repoClient = RepositoryHelper.Get客戶資料Repository();
            repoReport = RepositoryHelper.GetReportRepository();
            repoContact = RepositoryHelper.Get客戶聯絡人Repository();
            repoBank = RepositoryHelper.Get客戶銀行資訊Repository();
        }

        // GET: Client
        public ActionResult Index()
        {
            var data = repoClient.All();
            return View(data);
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶資料 = repoClient.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            ViewData["BankList"] = repoBank.GetAllDataByClientId(id.Value);
            ViewData["ContactList"] = repoContact.GetAllDataByClientId(id.Value);

            return View(客戶資料);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            var client = new 客戶資料();

            if (TryUpdateModel<I客戶資料新增>(client))
            {
                repoClient.AddData(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶資料 = repoClient.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            ViewData["BankList"] = repoBank.GetAllDataByClientId(id.Value);
            ViewData["ContactList"] = repoContact.GetAllDataByClientId(id.Value);

            return View(客戶資料);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IEnumerable<客戶銀行資訊> bank, IEnumerable<客戶聯絡人> contact)
        {
            var client = repoClient.Find(id);
            ViewData["BankList"] = bank;
            ViewData["ContactList"] = contact;

            if (!ModelState.IsValid)
            {
                return View(client);
            }

            if (TryUpdateModel<I客戶資料更新>(client))
            {
                client.客戶銀行資訊.ForEach(item =>
                {
                    var tempBank = bank.FirstOrDefault(a => a.Id == item.Id);
                    if (tempBank != null)
                    {
                        item.銀行名稱 = tempBank.銀行名稱;
                        item.銀行代碼 = tempBank.銀行代碼;
                        item.分行代碼 = tempBank.分行代碼;
                        item.帳戶名稱 = tempBank.帳戶名稱;
                        item.帳戶號碼 = tempBank.帳戶號碼;
                    }
                });

                client.客戶聯絡人.ForEach(item =>
                {
                    var tempContact = contact.FirstOrDefault(a => a.Id == item.Id);
                    if (tempContact != null)
                    {
                        item.職稱 = tempContact.職稱;
                        item.姓名 = tempContact.姓名;
                        item.Email = tempContact.Email;
                        item.手機 = tempContact.手機;
                        item.電話 = tempContact.電話;
                    }
                });

                repoClient.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = repoClient.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repoClient.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBank([Bind(Prefix = "item", Include = "Id,客戶Id")]客戶銀行資訊 bank)
        {
            repoBank.Delete(bank.Id);

            return RedirectToAction("Details", new { id = bank.客戶Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContact([Bind(Prefix = "item", Include = "Id,客戶Id")]客戶聯絡人 contact)
        {
            repoContact.Delete(contact.Id);

            return RedirectToAction("Details", new { id = contact.客戶Id });
        }

        public ActionResult Report()
        {
            var data = repoReport.All().ToList();
            return View(data);
        }

        public FileResult Export()
        {
            var list = repoClient.GetExport();
            var byteArray = ExportHelper.Exprot(list);

            var fileName = DateTime.Now.ToString("yyyyMMdd") + "_客戶資料匯出.xlsx";

            return File(byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoClient.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
