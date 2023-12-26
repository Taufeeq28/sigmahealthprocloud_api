using Data.Models;
using Microsoft.EntityFrameworkCore;


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
        public DbSet<GenerateNextIdResponse> GenerateNextIdResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenerateNextIdResponse>().HasNoKey();
            modelBuilder.Entity<FacilitySearchResponse>().HasNoKey();
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
