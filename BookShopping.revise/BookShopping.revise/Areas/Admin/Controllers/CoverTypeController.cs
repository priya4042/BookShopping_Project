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
    [Authorize(Roles =SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IunitofWork _unitofWork;
        public CoverTypeController(IunitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
                return View(coverType);
            coverType = _unitofWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null)
                return NotFound(coverType);
            return View(coverType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(coverType);
            if (coverType.Id == 0)
                _unitofWork.CoverType.Add(coverType);
            else
            _unitofWork.CoverType.update(coverType);
            _unitofWork.Save();
            return RedirectToAction("Index");

        }

        #region covertyperegion

       

        [HttpGet]
        public IActionResult GetAll()
        {
            var covertypeIndb = _unitofWork.CoverType.GetAll();
            return Json(new { data = covertypeIndb });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var covertypefromdb = _unitofWork.CoverType.Get(id);
            if (covertypefromdb == null)
                return Json(new { success = false, messgae = "error while delteing data" });
            _unitofWork.CoverType.Remove(covertypefromdb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data deleted successfully" });
        }
        #endregion
    }
}
