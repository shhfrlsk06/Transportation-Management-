using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TransportationManagement.Models;

namespace TransportationManagement.Controllers
{
    public class ShipmentsController : Controller
    {
        private TransportationDbContext db = new TransportationDbContext();
        const string googleApiKey = "AIzaSyAVTS-hRRBnBm46Ee0r0lo70uTiZqVt5PI";
        const double perLbRate = 2.01;
        const double fuelSurcharge = .18;
        const int dimensionalWeightDenominator = 166;  //Industry standard

        // GET: Shipments
        public ActionResult Index(string searchString)
        {
            var temp = from t in db.Shipments
                           select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                temp = temp.Where(s => s.ShipFrom_Company.Contains(searchString)||
                                       s.ShipFrom_Company.Contains(searchString)||
                                       s.ShipTo_Company.Contains(searchString)||
                                       s.ShipTo_Attention.Contains(searchString)||
                                       s.ShipFrom_EmailAddress.Contains(searchString));
            }
            return View(temp);
        }

        // GET: Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Shipments/Create

        public ActionResult Create(int? id)
        {

            if (id != null)
            {
                TempData["CompanyId"] = id;
            }
            return View("Quote");
        }

        public ActionResult Success(Guid TrackingId)
        {

            @ViewBag.trackingId = TrackingId;
            return View("Success");
        }
        public ActionResult Payment()
        {
            return View("Payment");
        }
        public double DetermineDistance(string ShipTo_City, string ShipFrom_City, string ShipTo_State, string ShipFrom_State)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://maps.googleapis.com/maps/api/distancematrix/xml?origins="+ShipFrom_City+"+"+ShipFrom_State+"&destinations="+ShipTo_City+"+"+ShipTo_State+"&mode=driving&language=En-EN&key="+googleApiKey);
            XDocument doc = XDocument.Load(stream);
            string distance = doc.Descendants()
                                .Where(a => a.Name.LocalName == "value")
                                .Last().Value;
            double metersPerMile = 1609.35;
            double distanceInMiles = Math.Round((Convert.ToDouble(distance) / metersPerMile),2);
            return distanceInMiles;
        }


        public void SendEmailNotification(Guid TrackingId, string ShipTo_EmailAddress)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("emailNotifications@gmail.com", "1234wasd"), 
                EnableSsl = true,
                Timeout = 10000
            };

            MailMessage message = new MailMessage();
            message.Body = ("Hello,  Your tracking id is: " + TrackingId.ToString());
            message.Subject = "Your Recent Shipment";
            message.To.Add(ShipTo_EmailAddress);
            message.From = new MailAddress("emailNotifications@gmail.com");
            smtp.Send(message);
        }

        public double DetermineDimensionalWeight(int Depth, int Width, int Height)
        {
            return (Depth * Width * Height / dimensionalWeightDenominator);
        }

        public double DeterminFreightCharge(double DistanceInMiles, double Weight)
        {
            return Math.Round((DistanceInMiles * fuelSurcharge) + (Weight * perLbRate), 2);
        }
        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Request Pickup")] 
        public ActionResult Create([Bind(Include = "ShipmentId,ShipTo_EmailAddress,ShipTo_Company,ShipTo_Attention,ShipTo_AddressLine1,ShipTo_AddressLine2,ShipTo_AddressLine3,ShipTo_City,ShipTo_State,ShipTo_PostalCode,ShipTo_Country,ShipFrom_EmailAddress,ShipFrom_Company,ShipFrom_AddressLine1,ShipFrom_AddressLine2,ShipFrom_AddressLine3,ShipFrom_City,ShipFrom_State,ShipFrom_PostalCode,ShipFrom_Country,Weight,Depth,Width,Height,ReferenceNumber1,ReferenceNumber2,ShipmentDate")] Shipment shipment, int? id, string Weight, string Depth, string Width, string Height, string ShipTo_City, string ShipFrom_City, string ShipTo_State, string ShipFrom_State)
        {   
            double distanceInMiles = DetermineDistance(ShipTo_City, ShipFrom_City, ShipTo_State, ShipFrom_State);
            double dimensionalWeight = DetermineDimensionalWeight(Convert.ToInt32(Depth), Convert.ToInt32(Width), Convert.ToInt32(Height));
            double weight = Convert.ToDouble(Weight);
            Guid trackingGuid = Guid.NewGuid();
            TempData["TrackingId"] = trackingGuid;
            id = Convert.ToInt32(TempData["CompanyId"]);
            if (dimensionalWeight > weight)
            {
                weight = dimensionalWeight; 
            }       
            double freightCharge = DeterminFreightCharge(distanceInMiles, weight);
            if (ModelState.IsValid)
            {
                db.Shipments.Add(shipment);
                db.Shipments.Add(shipment).InvoiceAmount = freightCharge;
                db.Shipments.Add(shipment).TrackingId=trackingGuid;
              //db.Companies.Find(id).Shipments.Add(shipment);
                db.SaveChanges();

                SendEmailNotification(shipment.TrackingId, shipment.ShipTo_EmailAddress);

            }
            return View("Success");
        }
        public ActionResult Quote(int? id)
        {
            if (id != null)
            {
                TempData["CompanyId"] = id;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Quote")] 
        public ActionResult Quote([Bind(Include = "ShipmentId,ShipTo_EmailAddress,ShipTo_Company,ShipTo_Attention,ShipTo_AddressLine1,ShipTo_AddressLine2,ShipTo_AddressLine3,ShipTo_City,ShipTo_State,ShipTo_PostalCode,ShipTo_Country,ShipFrom_EmailAddress,ShipFrom_Company,ShipFrom_AddressLine1,ShipFrom_AddressLine2,ShipFrom_AddressLine3,ShipFrom_City,ShipFrom_State,ShipFrom_PostalCode,ShipFrom_Country,Weight,Depth,Width,Height,ReferenceNumber1,ReferenceNumber2,ShipmentDate")] Shipment shipment, int? id, string Weight, string Depth, string Width, string Height, string ShipTo_City, string ShipFrom_City, string ShipTo_State, string ShipFrom_State)
        {
            double distanceInMiles = DetermineDistance(ShipTo_City, ShipFrom_City, ShipTo_State, ShipFrom_State);
            double dimensionalWeight = DetermineDimensionalWeight(Convert.ToInt32(Depth), Convert.ToInt32(Width), Convert.ToInt32(Height));
            double weight = Convert.ToDouble(Weight);
            Guid trackingGuid = Guid.NewGuid();
            if (dimensionalWeight > weight)
            {
                weight = dimensionalWeight;
            }
            double freightCharge = DeterminFreightCharge(distanceInMiles, weight);
            ViewBag.freightCharge = "$ "+freightCharge;

            return View(shipment);
        }
        // GET: Shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipmentId,ShipTo_EmailAddress,ShipTo_Company,ShipTo_Attention,ShipTo_AddressLine1,ShipTo_AddressLine2,ShipTo_AddressLine3,ShipTo_City,ShipTo_State,ShipTo_PostalCode,ShipTo_Country,ShipFrom_EmailAddress,ShipFrom_Company,ShipFrom_AddressLine1,ShipFrom_AddressLine2,ShipFrom_AddressLine3,ShipFrom_City,ShipFrom_State,ShipFrom_PostalCode,ShipFrom_Country,Weight,Depth,Width,Height,TrackingId,ReferenceNumber1,ReferenceNumber2,InvoiceAmount,ShipmentDate")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shipment shipment = db.Shipments.Find(id);
            db.Shipments.Remove(shipment);
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
