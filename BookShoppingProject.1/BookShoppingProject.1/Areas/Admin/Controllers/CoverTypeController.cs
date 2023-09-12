using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject._1.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            coverType = _unitofWork.coverType.Get(id.GetValueOrDefault());
            if (coverType == null)
                return NotFound();
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
                _unitofWork.coverType.Add(coverType);
            else
                _unitofWork.coverType.Update(coverType);
            _unitofWork.Save();
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
                return Json(new { success = false, message = "Error while deleting message" });
            _unitofWork.coverType.Remove(CoverTypeInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data successfully deleted" });
        }
        #endregion
    }
}
