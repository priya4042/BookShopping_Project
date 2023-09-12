using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_MVC_CORE_UnderStanding3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
            var CoverTypeInDb = _unitofWork.CoverType.Get(id.GetValueOrDefault());
            if (CoverTypeInDb == null)
                return NotFound();
            return View(CoverTypeInDb);
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
                _unitofWork.CoverType.Update(coverType);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }


        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var CoverTypeList = _unitofWork.CoverType.GetAll();
            return Json(new { data = CoverTypeList });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CoverTypeInDb = _unitofWork.CoverType.Get(id);
            if (CoverTypeInDb == null)
                return Json(new { success = false, message = "Error While Deleting data" });
            _unitofWork.CoverType.Remove(CoverTypeInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfully" });

        }
        #endregion
    }
}
