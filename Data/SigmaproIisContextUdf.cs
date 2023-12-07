using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public partial class SigmaproIisContextUdf : DbContext
    {
        public SigmaproIisContextUdf()
        {
        }

        public SigmaproIisContextUdf(DbContextOptions<SigmaproIisContextUdf> options)
            : base(options)
        {
        }
        public virtual DbSet<FacilitySearchResponse> FacilitySearch { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacilitySearchResponse>(e =>
            {
                e.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
