using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork02.Models;
using MVCHomeWork02.Controllers;

namespace MVCHomeWork02.Controllers
{
    public class BankController : BaseController
    {
        private readonly 客戶銀行資訊Repository repoBank;
        private readonly 客戶資料Repository repoClient;

        public BankController()
        {
            repoBank = RepositoryHelper.Get客戶銀行資訊Repository();
            repoClient = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: Bank
        public ActionResult Index()
        {
            var data = repoBank.GetAllData();
            return View(data);
        }

        // GET: Bank/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶銀行資訊 = repoBank.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: Bank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = repoClient.GetSelect(null);
            return View();
        }

        // POST: Bank/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            var bank = new 客戶銀行資訊();

            if (TryUpdateModel<I客戶銀行資訊新增>(bank))
            {
                repoBank.AddData(bank);
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = repoClient.GetSelect(bank.客戶Id);
            return View(bank);
        }

        // GET: Bank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶銀行資訊 = repoBank.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = repoClient.GetSelect(客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: Bank/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var bank = repoBank.Find(id);

            if (TryUpdateModel<I客戶銀行資訊更新>(bank))
            {
                repoBank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = repoClient.GetSelect(bank.客戶Id);
            return View(bank);
        }

        // GET: Bank/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶銀行資訊 = repoBank.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repoBank.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoBank.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
