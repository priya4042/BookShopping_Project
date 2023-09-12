using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShoping_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopingProject_11.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin +","+ SD.Role_Employee)]

    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CompanyController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
                return View(company);
            company = _unitofWork.company.Get(id.GetValueOrDefault());
            if (company == null)
                return NotFound();
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Company company)
        {
            if (company == null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(company);
            if (company.Id == 0)
                _unitofWork.company.Add(company);
            else
                _unitofWork.company.Update(company);
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }
        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.company.GetAll() });
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var companyInDb = _unitofWork.company.Get(id);
            if (companyInDb == null)
                return Json(new { success = false, message = "error while deleting data" });
            _unitofWork.company.Remove(companyInDb);
            _unitofWork.save();
            return Json(new { success = true, message = "Data Delete Successfully" });


        }
        #endregion
    }
}
