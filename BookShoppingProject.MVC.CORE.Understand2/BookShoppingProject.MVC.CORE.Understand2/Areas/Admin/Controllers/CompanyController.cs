using BookShopping.DataAccess.Repository.IRepository;
using BookShopping.Models;
using BookShopping.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject.MVC.CORE.Understand2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
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
            company = _unitofWork.Company.Get(id.GetValueOrDefault());
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
                _unitofWork.Company.Add(company);
            else
                _unitofWork.Company.Update(company);
            _unitofWork.save();
            return RedirectToAction("Index");
        }

        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var companyInDb = _unitofWork.Company.GetAll();
            return Json(new { data = companyInDb });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var companyfromdb = _unitofWork.Company.Get(id);
            if (companyfromdb == null)
                return Json(new { success = false, message = "error while deleting data" });

            _unitofWork.Company.Remove(companyfromdb);
            _unitofWork.save();
            return Json(new { success = true, message = "data deleted successfully" });
        }
        #endregion
    }
}
