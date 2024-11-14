using Azure;
using HealtSync.Application.Contracts.Users;
using HealtSync.Application.Dtos.Users.Doctors;
using HealtSync.Application.Dtos.Users.Patients;
using HealtSync.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.Web.Controllers
{
    public class PatientController : Controller
    {

        IPatientsService _patientsService;

        public PatientController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

       
        public async Task<IActionResult> Index()
        {
            var result = await _patientsService.GetAll();

            if (result.IsSuccess)
            {
                List<GetSimplePatientDto> patients =(List<GetSimplePatientDto>) result.Model!;

                return View(patients);
            }
            
            return View();
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _patientsService.GetById(id);

            if (result.IsSuccess)
            {
                GetDetailedPatientDto patient = (GetDetailedPatientDto)result.Model!;

                return View(patient);
            }

            return View();
        }

        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientSaveDto patientSaveDto)
        {

            var result = await _patientsService.SaveAsync(patientSaveDto);

            ViewBag.Message = result.Message;

            return result.IsSuccess ? RedirectToAction(nameof(Index)) : View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _patientsService.GetById(id);

            if (result.IsSuccess)
            {
                GetDetailedPatientDto patient = (GetDetailedPatientDto)result.Model!;

                return View(patient);
            }

            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientUpdateDto patientUpdateDto)
        {
            var result = await _patientsService.UpdateAsync(patientUpdateDto);

            ViewBag.Message = result.Message;

            return result.IsSuccess ? RedirectToAction(nameof(Index)) : View();
        }

     
    }
}
