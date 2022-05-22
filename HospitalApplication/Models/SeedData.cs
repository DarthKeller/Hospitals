using Microsoft.EntityFrameworkCore;

namespace HospitalApplication.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HospitalContext(
                serviceProvider.GetRequiredService<DbContextOptions<HospitalContext>>()))
            {
                if(context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                var modifiedContext = false;

                if (!context.Hospitals.Any())
                {
                    modifiedContext = true;

                    context.Hospitals.Add(new Hospital
                    {
                        Address = "123 Main St",
                        City = "Oklahoma City",
                        State = "OK",
                        Name = "First Hospital",
                        MaxBeds= 10
                    }) ;

                }

                if (!context.Specialties.Any())
                {
                    modifiedContext = true;

                    context.Specialties.AddRange(
                        new Specialty { Name = "Cardiology" },
                        new Specialty { Name = "Neurology" },
                        new Specialty { Name = "Obstetrics" },
                        new Specialty { Name = "Pediatrics" },
                        new Specialty { Name = "Oncology" },
                        new Specialty { Name = "Psychiatric" },
                        new Specialty { Name = "Trauma" }
                        );
                }

                if (!context.InsuranceProviders.Any())
                {
                    modifiedContext = true;
                    context.InsuranceProviders.AddRange(
                        new InsuranceProvider { Name = "Blue Cross Blue Shield (BCBS)" },
                        new InsuranceProvider { Name = "United Healthcare" },
                        new InsuranceProvider { Name = "TriCare" },
                        new InsuranceProvider { Name = "Global Health" },
                        new InsuranceProvider { Name = "Aetna" },
                        new InsuranceProvider { Name = "Humana" }
                        );
                }

                if(modifiedContext)
                    context.SaveChanges();
            }
        }
    }
}
