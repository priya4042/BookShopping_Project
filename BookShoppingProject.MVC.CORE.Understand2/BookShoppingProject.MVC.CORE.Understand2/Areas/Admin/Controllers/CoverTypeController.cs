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
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public CoverTypeController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
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
            var CoverTypeInDb = _unitofWork.coverType.Get(id.GetValueOrDefault());
            if (CoverTypeInDb == null)
                return NotFound();
            return View(CoverTypeInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert (CoverType coverType)
        {
            if (coverType == null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(coverType);
            if (coverType.Id == 0)
                _unitofWork.coverType.Add(coverType);
            else
                _unitofWork.coverType.Update(coverType);
            _unitofWork.save();
            return RedirectToAction(nameof(Index));

        }

        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var CoverTypeList = _unitofWork.coverType.GetAll();
            return Json(new { data = CoverTypeList });
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CoverTypeInDb = _unitofWork.coverType.Get(id);
            if (CoverTypeInDb == null)
                return Json(new { success = false, message = "Error While Deleting data" });
            _unitofWork.coverType.Remove(CoverTypeInDb);
            _unitofWork.save();
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }

        #endregion
    }
}
