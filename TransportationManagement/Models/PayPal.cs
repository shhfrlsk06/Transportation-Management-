﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TransportationManagement.Models
{
    public class PayPal
    {
        public string cmd { get; set; }
        public string business { get; set; }
        public string no_shipping { get; set; }
        public string @return { get; set; }
        public string cancel_return { get; set; }
        public string notify_url { get; set; }
        public string currency_code { get; set; }
        public string item_name { get; set; }
        public string amount { get; set; }
        public string actionURL { get; set; }
        public PayPal(bool useSandbox)
        {
            this.cmd = "_xclick";
            this.business = ConfigurationManager.AppSettings["business"];
            this.cancel_return = ConfigurationManager.AppSettings["cancel_return"];
            this.@return = ConfigurationManager.AppSettings["return"];
            if (useSandbox)
            {
                this.actionURL = ConfigurationManager.AppSettings["test_url"];
            }
            else
            {
                this.actionURL = ConfigurationManager.AppSettings["Prod_url"];
            }
           
            this.notify_url = ConfigurationManager.AppSettings["notify_url"];
            
            this.currency_code = ConfigurationManager.AppSettings["currency_code"];
        }
    }
}