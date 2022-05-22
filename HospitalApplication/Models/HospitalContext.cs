using Microsoft.EntityFrameworkCore;

namespace HospitalApplication.Models
{
    public class HospitalContext: DbContext
    {

        public HospitalContext(DbContextOptions<HospitalContext> options):base(options)
        {

        }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        public DbSet<Specialty> Specialties { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hospital>(h => {
                h.Property(c => c.Name).HasMaxLength(50);
                h.Property(c => c.Name).IsRequired(true);
                h.Property(c => c.MaxBeds).IsRequired(true);
            });

            builder.Entity<Specialty>(s => {
                s.Property(c => c.Name).IsRequired(true);
            });

            builder.Entity<InsuranceProvider>(i => {
                i.Property(c => c.Name).IsRequired(true);
            });
            
        }

    }
}
