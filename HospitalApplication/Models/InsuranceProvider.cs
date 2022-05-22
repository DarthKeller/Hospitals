using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApplication.Models
{
    public class InsuranceProvider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Hospital> InNetworkHospitals { get; set; }
    }
}
