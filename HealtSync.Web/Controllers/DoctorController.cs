using HealtSync.Application.Contracts.Users;
using HealtSync.Application.Dtos.Users.Doctors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HealtSync.Web.Controllers
{
    public class DoctorController : Controller
    {
        IDoctorsService _doctorService;

        public DoctorController(IDoctorsService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _doctorService.GetAll();

            if (result.IsSuccess)
            {
                List<GetSimpleDoctorDto> doctors = (List<GetSimpleDoctorDto>)result.Model!;

                return View(doctors);
            }

            return View();

        }

        public async Task<IActionResult> Details(int id)
        {

            var result = await _doctorService.GetById(id);

            if (result.IsSuccess)
            {
                GetDetailedDoctorDto doctor = (GetDetailedDoctorDto)result.Model!;

                return View(doctor);
            }


            return View();
        }



        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorSaveDto doctorSaveDto)
        {

            try
            {
                doctorSaveDto.ChangeDate = DateTime.Now;
                var result = await _doctorService.SaveAsync(doctorSaveDto);



                ViewBag.Message = result.Message;

                return result.IsSuccess ? RedirectToAction(nameof(Index)) : View();

            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await _doctorService.GetById(id);

            if (result.IsSuccess)
            {
                GetDetailedDoctorDto doctor = (GetDetailedDoctorDto)result.Model!;
                return View(doctor);
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorUpdateDto doctorUpdateDto)
        {

            try
            {
                doctorUpdateDto.ChangeDate = DateTime.Now;
                var result = await _doctorService.UpdateAsync(doctorUpdateDto);
                

                
                ViewBag.Message = result.Message;

                return result.IsSuccess ? RedirectToAction(nameof(Index)) : View();

            }
            catch
            {
                return View();
            }
        }
    }
}
