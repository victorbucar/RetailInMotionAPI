using Microsoft.EntityFrameworkCore;
using RetailInMotion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailInMotion.Data.DataContext
{
    public class RetailContext : DbContext
    {
        public RetailContext(DbContextOptions<RetailContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
