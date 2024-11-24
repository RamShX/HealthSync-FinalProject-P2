using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.Users.Api.Controllers
{
    public class DoctorApmController : Controller
    {
        // GET: DoctorApmController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoctorApmController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
