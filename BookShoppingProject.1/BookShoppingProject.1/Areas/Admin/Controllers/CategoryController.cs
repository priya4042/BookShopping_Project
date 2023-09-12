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
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
                return View(category);
            var CategoryList = _unitofWork.Category.Get(id.GetValueOrDefault());
            if (CategoryList == null)
                return NotFound();
            return View(CategoryList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Category category)
        {
            if (category == null)
                return View();
            if (!ModelState.IsValid)
                return View(category);
            if (category.Id == 0)
                _unitofWork.Category.Add(category);
            else
                _unitofWork.Category.Update(category);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }

        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var CategoryList = _unitofWork.Category.GetAll();
            return Json(new { data = CategoryList });
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CategoryInDb = _unitofWork.Category.Get(id);
            if (CategoryInDb == null)
                return Json(new { success = false, message = "Error while deleteing message" });
            _unitofWork.Category.Remove(CategoryInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data Successfully deleted" });

        }
        #endregion
    }
}
