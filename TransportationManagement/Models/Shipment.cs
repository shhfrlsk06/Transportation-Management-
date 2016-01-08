using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TransportationManagement.Models
{
    public class Shipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipmentId { get; set; }
        [Display(Name = "Email Address")]
        public string ShipTo_EmailAddress { get; set; }
        [Display(Name = "Company Name")]
        public string ShipTo_Company { get; set; }
        [Display(Name = "Attention")]
        public string ShipTo_Attention { get; set; }
        [Display(Name = "Address Line 1")]
        public string ShipTo_AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string ShipTo_AddressLine2 { get; set; }
        [Display(Name = "Address Line 3")]
        public string ShipTo_AddressLine3 { get; set; }
        [Display(Name = "City")]
        public string ShipTo_City { get; set; }
        [Display(Name = "State")]
        public string ShipTo_State { get; set; }
        [Display(Name = "Postal Code")]
        public string ShipTo_PostalCode { get; set; }
        [Display(Name = "Country")]
        public string ShipTo_Country { get; set; }
        [Display(Name = "Email Address")]
        public string ShipFrom_EmailAddress { get; set; }
        [Display(Name = "Company Name")]
        public string ShipFrom_Company { get; set; }
        [Display(Name = "Address Line 1")]
        public string ShipFrom_AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string ShipFrom_AddressLine2 { get; set; }
        [Display(Name = "Address Line 3")]
        public string ShipFrom_AddressLine3 { get; set; }
        [Display(Name = "City")]
        public string ShipFrom_City { get; set; }
        [Display(Name = "State")]
        public string ShipFrom_State { get; set; }
        [Display(Name = "Postal Code")]
        public string ShipFrom_PostalCode { get; set; }
        [Display(Name = "Country")]
        public string ShipFrom_Country { get; set; }
        public double Weight { get; set; }
        public int Depth { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid TrackingId { get; set; }
        public string ReferenceNumber1 { get; set; }
        public string ReferenceNumber2 { get; set; }
        public enum ServiceLevel { Standard=1, Expedited=2 }
        public double InvoiceAmount { get; set; }
        public enum FreightTerms { FoB, CoD }
        public DateTime ShipmentDate { get; set; }
    }
}