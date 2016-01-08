using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TransportationManagement.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DHXJson(Alias = "id")]
        public int Id { get; set; }
        public string Name { get; set; }
        [DHXJson(Alias = "text")]
        public string Description { get; set; }
        [DHXJson(Alias = "start_date")]
        public DateTime StartDate { get; set; }
        [DHXJson(Alias = "end_date")]
        public DateTime EndDate { get; set; }
    }
}