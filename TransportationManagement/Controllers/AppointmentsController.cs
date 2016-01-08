using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransportationManagement.Models;
using DHTMLX.Scheduler;

namespace TransportationManagement.Controllers
{
    public class AppointmentsController : Controller
    {
        private TransportationDbContext db = new TransportationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Glossy;
            return View(scheduler);
        }

    //    // GET: Appointments/Details/5
    //    public ActionResult Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Appointment appointment = db.Appointments.Find(id);
    //        if (appointment == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(appointment);
    //    }

    //    // GET: Appointments/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Appointments/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Appointment appointment)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Appointments.Add(appointment);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        return View(appointment);
    //    }

    //    // GET: Appointments/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Appointment appointment = db.Appointments.Find(id);
    //        if (appointment == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(appointment);
    //    }

    //    // POST: Appointments/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Appointment appointment)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(appointment).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(appointment);
    //    }

    //    // GET: Appointments/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        Appointment appointment = db.Appointments.Find(id);
    //        if (appointment == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(appointment);
    //    }

    //    // POST: Appointments/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        Appointment appointment = db.Appointments.Find(id);
    //        db.Appointments.Remove(appointment);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    }
}
