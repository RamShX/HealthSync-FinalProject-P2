using HealtSync.Web.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HealtSync.Web.Controllers
{
    public class DoctorAdmController : Controller
    {
        // GET: DoctorApmController
        public async Task<IActionResult> Index()
        {
            string baseUrl = "http://localhost:5289/api/";

            DoctorGetAllResultModel doctorGetAllResultModel = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var responseTask = await client.GetAsync("Doctors/GetDoctors");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();

                    doctorGetAllResultModel = JsonConvert.DeserializeObject<DoctorGetAllResultModel>(response)!;
                    

                    return View(doctorGetAllResultModel.Data);
                }
                else
                {
                    ViewBag.Message(doctorGetAllResultModel.Message);
                    
                }
            }


            return View();
        }

        // GET: DoctorApmController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string baseUrl = "http://localhost:5289/api/";

            DoctorGetByIdResultModel doctorGetByIdResultModel = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var responseTask = await client.GetAsync($"Doctors/GetDoctorById?id={id}");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();

                    doctorGetByIdResultModel = JsonConvert.DeserializeObject<DoctorGetByIdResultModel>(response)!;

                    return View(doctorGetByIdResultModel.Data);
                }
                else
                {
                    ViewBag.Message(doctorGetByIdResultModel.Message);
                }

                return View();
            }
        }

        // GET: DoctorApmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorApmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorApmController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorApmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
