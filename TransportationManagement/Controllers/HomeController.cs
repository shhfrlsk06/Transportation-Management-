using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Common;
using TransportationManagement.Models;
using System.Data.Entity;

namespace TransportationManagement.Controllers
{
    public class HomeController : Controller
    {
        private TransportationDbContext db = new TransportationDbContext();
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Terrace;
 
            scheduler.Config.first_hour = 4;
            scheduler.Config.last_hour = 22;
 
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
 
            return View(scheduler);
        }
 
        public ContentResult Data()
        {
            var apps = db.Appointments.ToList();
            return new SchedulerAjaxData(apps);
        }
 
        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
 
            try
            {
                var changedEvent = DHXEventsHelper.Bind<Appointment>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        db.Appointments.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        db.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:// "update"  
                        db.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                db.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }
 
            return (new AjaxSaveResponse(action));
        }
 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }   
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}