using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Web.Models;
using HealtSync.Web.Services.Users;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace HealtSync.Web.Controllers
{
    public class DoctorAdmController : Controller
    {

        private readonly IDoctorApiClientService _doctorClientService;

        public DoctorAdmController(IDoctorApiClientService doctorClientService)
        {
            _doctorClientService = doctorClientService; 
        }

        public async Task<IActionResult> Index()
        {

            DoctorGetAllResultModel doctorGetAllResultModel =await _doctorClientService.GetDoctors();

            return View(doctorGetAllResultModel.Data);
        }

        // GET: DoctorApmController/Details/5
        public async Task<IActionResult> Details(int id)
        {
          
            DoctorGetByIdResultModel doctorGetByIdResultModel = await _doctorClientService.GetDoctorGetById(id);

            return View(doctorGetByIdResultModel.Data);
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
