using HospitalApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using HospitalApplication.Extensions;

namespace HospitalApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HospitalContext _context;

        public HomeController(ILogger<HomeController> logger, HospitalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var hospitals = _context.Hospitals;

            return View(hospitals.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Read(int id) {
            var hospital = _context.Hospitals.Include(c=>c.Specialties).Include(c=>c.PreferredProviders).First(c => c.Id == id);

            return View(hospital);
        }

        public IActionResult Delete(int id) { 
            var hospital = _context.Hospitals.First(c=>c.Id== id);

            return View(hospital); }

        public IActionResult Edit(int id) {
            var hospital = _context.Hospitals.Include(c => c.Specialties).Include(c => c.PreferredProviders).First(c => c.Id == id);
            var specialties = _context.Specialties;
            var providers = _context.InsuranceProviders;

            hospital.dropDownSpecialties= specialties.ToSelectList(hospital.Specialties);
            hospital.dropDownProviders = providers.ToSelectList(hospital.PreferredProviders);

            return View(hospital); 
        }

        public IActionResult Create()
        {
            var hospital = new Hospital();

            var specialties = _context.Specialties;
            var providers = _context.InsuranceProviders;

            hospital.dropDownSpecialties = specialties.ToSelectList();
            hospital.dropDownProviders = providers.ToSelectList();

            return View(hospital);
        }

        #region Posts

        [HttpPost]
        public IActionResult Create([FromForm] Hospital hospital, IFormCollection collection)
        {

            var stringSpecIds = collection["dropDownSpecialties"];
            var stringProvIds = collection["dropDownProviders"];

            List<int> specIds = new List<int>();

            foreach (var s in stringSpecIds.ToArray())
            {
                specIds.Add(Int32.Parse(s));
            }

            List<int> provIds = new List<int>();
            foreach (var p in stringProvIds.ToArray())
            {
                provIds.Add(Int32.Parse(p));
            }

            var specialties = GetSpecialtiesByArrayOfIds(specIds);
            var providers = GetProvidersByArrayofIds(provIds);

            hospital.Specialties = specialties;
            hospital.PreferredProviders = providers;

            _context.Add(hospital);
            _context.SaveChanges();


            return RedirectToAction("Read", new { id = hospital.Id });
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Hospital hospital, IFormCollection collection)
        {
            var contextHospital = _context.Hospitals.Include(c=>c.Specialties).Include(c=>c.PreferredProviders).First(c => c.Id == hospital.Id);

            var stringSpecIds = collection["dropDownSpecialties"];
            var stringProvIds = collection["dropDownProviders"];

            List<int> specIds = new List<int>();

            foreach (var s in stringSpecIds.ToArray())
            {
                specIds.Add(Int32.Parse(s));
            }

            List<int> provIds = new List<int>();
            foreach (var p in stringProvIds.ToArray())
            {
                provIds.Add(Int32.Parse(p));
            }

            var specialties = GetSpecialtiesByArrayOfIds(specIds);
            var providers = GetProvidersByArrayofIds(provIds);

            SortOutProviders(providers, contextHospital);
            SortOutSpecialties(specialties, contextHospital);

contextHospital.Address = hospital.Address;
            contextHospital.City = hospital.City;
            contextHospital.State = hospital.State;
            contextHospital.MaxBeds= hospital.MaxBeds;
            contextHospital.Name = hospital.Name;

                _context.SaveChanges();

            return RedirectToAction("Read", new {id=hospital.Id});
        }

        [HttpPost]
        public IActionResult Delete([FromForm] Hospital hospital)
        {
            hospital.PreferredProviders = null;
            hospital.Specialties = null;

            _context.Remove(hospital);
            _context.SaveChanges(true);

            return RedirectToAction("Index");
        }
        #endregion

        #region APIs
        public JsonResult GetSpecialties()
        {
            var specialties = _context.Specialties.OrderBy(c => c.Name);

            return Json(specialties);
        }

        public JsonResult GetInsuranceProviders()
        {
            var insurance = _context.InsuranceProviders.OrderBy(c => c.Name);

            return Json(insurance);
        }
        #endregion

        #region Private Methods
        private void SortOutSpecialties(List<Specialty> selectedSpecialties, Hospital hospital)
        {
            foreach(var specialty in hospital.Specialties)
            {
                hospital.Specialties.Remove(specialty);
            }

            foreach(var specialty in selectedSpecialties)
            {
                hospital.Specialties.Add(specialty);
            }
        }

        private void SortOutProviders(List<InsuranceProvider> selectedProviders, Hospital hospital)
        {
            foreach (var provider    in hospital.PreferredProviders)
            {
                hospital.PreferredProviders.Remove(provider);
            }

            foreach (var provider in selectedProviders)
            {
                hospital.PreferredProviders.Add(provider);
            }
        }

        private List<Specialty> GetSpecialtiesByArrayOfIds(List<int> ids)
        {
            var specialties = new List<Specialty>();

            foreach (var id in ids)
            {
                var specialty = _context.Specialties.First(_ => _.Id == id);
                specialties.Add(specialty);
            }

            return specialties;
        }

        private List<InsuranceProvider> GetProvidersByArrayofIds(List<int> ids)
        {
            var providers = new List<InsuranceProvider>();

            foreach (var id in ids)
            {
                var provider = _context.InsuranceProviders.First(_ => _.Id == id);
                providers.Add(provider);
            }

            return providers;
        }
        #endregion

    }
}