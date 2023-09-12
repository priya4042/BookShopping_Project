using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject._4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _UnitofWork;
        public CoverTypeController(IUnitofWork unitofWork)
        {
            _UnitofWork = unitofWork;
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
            coverType = _UnitofWork.coverType.Get(id.GetValueOrDefault());
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
                _UnitofWork.coverType.Add(coverType);
            else
            
                _UnitofWork.coverType.Update(coverType);
                _UnitofWork.Save();
                
            
            return RedirectToAction(nameof(Index));
        }


        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var CoverTypeList = _UnitofWork.coverType.GetAll();
            return Json(new { data = CoverTypeList });
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CovertypeList = _UnitofWork.coverType.Get(id);
            if (CovertypeList == null)
                return Json(new { success = false, message = "error while deleting data" });
            _UnitofWork.coverType.Remove(CovertypeList);
            _UnitofWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfully!!!" });
        }
        #endregion
    }
}
