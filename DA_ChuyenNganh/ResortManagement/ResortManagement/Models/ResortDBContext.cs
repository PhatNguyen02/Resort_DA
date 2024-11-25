using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ResortManagement.Models
{
    public class ResortDBContext : DbContext
    {
        public ResortDBContext() : base("MyConnectionString") { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Promotions> Promotions { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<UsedServices> UsedServices { get; set; }
    }
}