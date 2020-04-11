using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace CustomerDetails.Models
{
    public class CustomerDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerDbContext() : base("DefaultConnection")// database connection for dbcontext
        {
           
        }
    }
}