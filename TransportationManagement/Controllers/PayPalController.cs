using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationManagement.Models;

namespace TransportationManagement.Controllers
{
    public class PayPalController : Controller
    {
        public ActionResult RedirectFromPaypal()
        {
            return View();
        }
        public ActionResult CancelFromPaypal()
        {
            return View();
        }
        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
        public ActionResult ValidatePayPal(string product, string totalPrice)
        {
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
            var paypal = new PayPal(useSandbox);
            paypal.item_name = product;
            paypal.amount = totalPrice;
            return View(paypal);
        }
    }
}