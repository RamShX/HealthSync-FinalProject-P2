using HealtSync.Application.Contracts.Users;
using HealtSync.Application.Dtos.Users.Employees;
using HealtSync.Application.Dtos.Users.Employees.DtoConverters;
using HealtSync.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.Web.Controllers
{
    public class EmployeeController : Controller
    {

        IEmployeesService _employeeService;

        public EmployeeController(IEmployeesService employeerService)
        {
            _employeeService = employeerService;
        }


        public async Task<IActionResult> Index()
        {

            var result = await _employeeService.GetAll();

            if (result.IsSuccess)
            {
                List<GetSimpleEmployeeDto> employees = (List<GetSimpleEmployeeDto>)result.Model!;
                return View(employees);
            }
            return View();
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _employeeService.GetById(id);

            if (result.IsSuccess)
            {
                GetDetailedEmployeeDto employee = (GetDetailedEmployeeDto)result.Model!;
                return View(employee);
            }

            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();

        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeSaveDto employeeSaveDto)
        {

            var result = await _employeeService.SaveAsync(employeeSaveDto);

            ViewBag.Message = result.Message;

            return result.IsSuccess ? RedirectToAction(nameof(Index)) : View();

        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _employeeService.GetById(id);

            if(result.IsSuccess)
            {
                return View(result.Model);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateDto employeeUpdateDto)
        {
            var result = await _employeeService.UpdateAsync(employeeUpdateDto);

            ViewBag.Message = result.Message;

            return result.IsSuccess ? RedirectToAction(nameof(Index)) : View();
        }
       
    }
}
