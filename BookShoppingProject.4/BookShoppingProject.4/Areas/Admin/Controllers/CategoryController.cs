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

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
                return View(category);
            var CategoryInDb = _unitofWork.category.Get(id.GetValueOrDefault());
            if (CategoryInDb == null)
                return NotFound();
            return View(CategoryInDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Category category)
        {
            if (category == null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(category);
            if (category.Id == 0)
                _unitofWork.category.Add(category);
            else
                _unitofWork.category.Update(category);
            _unitofWork.Save();
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
            var categoryInDb = _unitofWork.category.Get(id);
            if (categoryInDb == null)
                return Json(new { Success = false, message = "Error while delete data" });
            _unitofWork.category.Remove(categoryInDb);
            _unitofWork.Save();
            return Json(new { Success = true, message = "Data Success delete!!!" });
        }
        #endregion
    }
}
