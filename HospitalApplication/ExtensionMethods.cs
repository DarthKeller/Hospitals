using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApplication.Extensions
{
    public static class ExtensionMethods
    {
        public static IList<SelectListItem> ToSelectList(this DbSet<Specialty> specialties)
        {
            var returnValue = new List<SelectListItem>();

            foreach(var specialty in specialties)
            {
                returnValue.Add(new SelectListItem(specialty.Name, specialty.Id.ToString()));
            }

            return returnValue;
        }

        public static IList<SelectListItem> ToSelectList(this DbSet<Specialty> specialties, ICollection<Specialty> selectedSpecialties)
        {
            var returnValue = new List<SelectListItem>();

            foreach (var specialty in specialties)
            {
                if (selectedSpecialties.Contains(specialty))
                {
                    returnValue.Add(new SelectListItem(specialty.Name, specialty.Id.ToString(), true));
                }
                else
                {
                    returnValue.Add(new SelectListItem(specialty.Name, specialty.Id.ToString(), false));
                }
                
            }

            return returnValue;
        }

        public static IList<SelectListItem> ToSelectList(this DbSet<InsuranceProvider> providers)
        {
            var returnValue = new List<SelectListItem>();

            foreach (var provider in providers)
            {
                returnValue.Add(new SelectListItem(provider.Name, provider.Id.ToString()));
            }

            return returnValue;
        }

        public static IList<SelectListItem> ToSelectList(this DbSet<InsuranceProvider> providers, ICollection<InsuranceProvider> selectedProviders)
        {
            var returnValue = new List<SelectListItem>();

            foreach (var provider in providers)
            {
                if (selectedProviders.Contains(provider))
                {
                    returnValue.Add(new SelectListItem(provider.Name, provider.Id.ToString(), true));
                }
                else
                {
                    returnValue.Add(new SelectListItem(provider.Name, provider.Id.ToString(), false));
                }
                
            }

            return returnValue;
        }
    }
}
