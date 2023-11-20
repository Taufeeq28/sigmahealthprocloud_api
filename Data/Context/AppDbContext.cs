using Microsoft.EntityFrameworkCore;
using System;
using Data.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<Jurisdictions> Jurisdictions { get; set; }
        public DbSet<Facilities> Facilities { get; set; }
        public DbSet<Counties> Counties { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Juridictions_organization_association> Juridictions_organization_association { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<User_Types> User_Types { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Contacts_Type> Contacts_Types { get; set; }
        public DbSet<LOV_type_master> LOV_type_master { get; set; }

    }
}
