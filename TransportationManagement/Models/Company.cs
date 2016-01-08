using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TransportationManagement.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        [Required]
        [MinLength(4)]
        public string ClientNumber { get; set; }
        public string Industry { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public virtual List<Contact> Contacts { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Shipment> Shipments { get; set; }
    }
}