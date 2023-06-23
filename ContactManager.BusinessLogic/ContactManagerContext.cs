using Bogus.DataSets;
using ContactManager.BusinessLogic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BusinessLogic
{
    public class ContactManagerContext : DbContext
    {
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(DataGenerator.Contacts);
            modelBuilder.Entity<Address>().HasData(DataGenerator.Addresses);
            base.OnModelCreating(modelBuilder);
        }
    }
}