using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Web.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

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


        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorApmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorSaveDto doctorSaveDto)
        {
            BaseModel model = new();

            try
            {
                string baseUrl = "http://localhost:5289/api/";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var responseTask = await client.PostAsJsonAsync<DoctorSaveDto>("Doctors/SaveDoctor", doctorSaveDto);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();

                        return RedirectToAction(nameof(Index));


                    }
                    else
                    {
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {

           return await Details(id);
        }

        // POST: DoctorApmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorUpdateDto doctorUpdateDto)
        {
            BaseModel model = new();

            try
            {
                string baseUrl = "http://localhost:5289/api/";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var responseTask = await client.PutAsJsonAsync<DoctorUpdateDto>("Doctors/UpdateDoctor", doctorUpdateDto);

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string response = await responseTask.Content.ReadAsStringAsync();

                        model = JsonConvert.DeserializeObject<BaseModel>(response)!;



                        if (!model.IsSuccess)
                        {
                            ViewBag.Message(model.Message);
                            return View();
                        }

                    }
                    else
                    {
                        return View();
                    }

                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
