using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportationManagement.Models
{
    public class TransportationDbContext:DbContext 
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}