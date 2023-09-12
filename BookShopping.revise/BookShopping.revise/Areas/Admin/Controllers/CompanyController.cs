using BookShopping_DataAccess.Repository.IRepository;
using BookShopping_Models;
using BookShopping_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.revise.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly IunitofWork _unitofWork;
        public CompanyController(IunitofWork unitofwork)
        {
            _unitofWork = unitofwork;
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
            _unitofWork.Save();
            return RedirectToAction("Index");
           
        }



        #region CompanyRegion
        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyIndb = _unitofWork.Company.GetAll();
            return Json(new { data = CompanyIndb });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var companyfromfb = _unitofWork.Company.Get(id);
            if (companyfromfb == null)
                return Json(new { success = false, message = "error while delteing data" });
            _unitofWork.Company.Remove(companyfromfb);
            _unitofWork.Save();
            return Json(new { success = true, message = "data delted successfully" });
        }
        public IActionResult Edit(Company company)
        {
            var companyfromfb = _unitofWork.Company.GetAll();
            if (companyfromfb == null)
                return Json(new { success = false, message = "error while delteing data" });
            _unitofWork.Company.Update(company);
            _unitofWork.Save();
           
            return Json(new { success = true, message = "data delted successfully" });
        }
        #endregion
    }
}
