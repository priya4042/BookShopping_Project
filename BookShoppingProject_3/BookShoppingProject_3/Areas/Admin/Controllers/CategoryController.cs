using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitWork _unitofWork;
        public CategoryController(IUnitWork unitWork)
        {
            _unitofWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult upsert(int? id)
        {
            category category = new category();
            if (id == null)
                return View(category);
            var CategoryInDb = _unitofWork.category.Get(id.GetValueOrDefault());
            if (CategoryInDb == null)
                return NotFound();
            return View(CategoryInDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(category category)
        {
            if (category == null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(category);
            if (category.Id == 0)
                _unitofWork.category.Add(category);
            else
                _unitofWork.category.Update(category);
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var CategoryList = _unitofWork.category.GetAll();
            return Json(new { data = CategoryList });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CategoryInDb = _unitofWork.category.Get(id);
            if (CategoryInDb == null)
                return Json(new { success = false, message = "Error While Deleteing Data" });
            _unitofWork.category.Remove(CategoryInDb);
            _unitofWork.save();
            return Json(new { success = true, message = "Data Deleted successfully" });
        }
        #endregion
    }
}
