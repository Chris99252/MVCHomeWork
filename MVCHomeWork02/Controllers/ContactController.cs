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
    public class ContactController : BaseController
    {
        private readonly 客戶聯絡人Repository repoContact;
        private readonly 客戶資料Repository repoClient;

        public ContactController()
        {
            repoContact = RepositoryHelper.Get客戶聯絡人Repository();
            repoClient = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: Contact
        public ActionResult Index()
        {
            var data = repoContact.GetAllData();

            return View(data);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = repoClient.GetSelect(null);
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            var contact = new 客戶聯絡人();

            if (TryUpdateModel<I客戶聯絡人新增>(contact))
            {
                repoContact.AddData(contact);
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {

            }

            ViewBag.客戶Id = repoClient.GetSelect(contact.客戶Id);
            return View(contact);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = repoClient.GetSelect(客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var 客戶聯絡人 = repoContact.Find(id);

            if (TryUpdateModel<I客戶聯絡人更新>(客戶聯絡人))
            {
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = repoClient.GetSelect(客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repoContact.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoContact.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
