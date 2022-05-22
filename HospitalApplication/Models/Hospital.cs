using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApplication.Models
{
    public class Hospital
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [System.ComponentModel.DisplayName("Maximum # of Beds")]
        public int MaxBeds { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public ICollection<Specialty> Specialties { get; set; }
        public ICollection<InsuranceProvider> PreferredProviders { get; set; }

        [NotMapped]
        public ICollection<SelectListItem> dropDownSpecialties { get; set; }

        [NotMapped]
        public ICollection<SelectListItem> dropDownProviders { get; set; }
    }
}
