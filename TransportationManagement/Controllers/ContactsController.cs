using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransportationManagement.Models;

namespace TransportationManagement.Controllers
{
    public class ContactsController : Controller
    {
        private TransportationDbContext db = new TransportationDbContext();

        // GET: Contacts
        public ActionResult Index(string searchString)
        {
            var temp = from t in db.Contacts
                       select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                temp = temp.Where(s => s.FirstName.Contains(searchString) ||
                                       s.PhoneNumber.Contains(searchString) ||
                                       s.LastName.Contains(searchString));
            }
            return View(temp);
        }

        public ActionResult CompanyContacts(string id)
        {
            Contact contact = db.Contacts.Find(id);
            return View();
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
       [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                TempData["CompanyId"] = id;
            }
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactID,FirstName,LastName,MiddleName,Title,PhoneNumber")] Contact contact, int? id)
        {
            id = Convert.ToInt32(TempData["CompanyId"]);
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.Companies.Find(id).Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index"); // ER change to redirect to company
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID,FirstName,LastName,MiddleName,Title,PhoneNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
