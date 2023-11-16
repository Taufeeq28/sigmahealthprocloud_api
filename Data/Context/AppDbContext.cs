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
        public DbSet<cities> Cities { get; set; }
        public DbSet<organizations> organizations { get; set; }
        public DbSet<jurisdictions> Jurisdictions { get; set; }
        public DbSet<facilities> facilities { get; set; }
        public DbSet<counties> Counties { get; set; }
        public DbSet<states> states { get; set; }
        public DbSet<countries> Countries { get; set; }
        public DbSet<Juridictions_organization_association> jurd_org_ass { get; set; }
        public DbSet<users> users { get; set; }

        public DbSet<user_types> user_types { get; set; }
    }
}
