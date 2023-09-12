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
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _UnitofWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _UnitofWork = unitofWork;
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
            var categoryInDb = _UnitofWork.Category.Get(id.GetValueOrDefault());
            if (categoryInDb == null)
                return NotFound();
            return View(categoryInDb);

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
                _UnitofWork.Category.Add(category);
            else
                _UnitofWork.Category.Update(category);
            _UnitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _UnitofWork.Category.GetAll();
            return Json(new { data = categoryList });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CategoryInDb = _UnitofWork.Category.Get(id);
            if (CategoryInDb == null)
                return Json(new { success = false, message = "Error while deleting data" });
            _UnitofWork.Category.Remove(CategoryInDb);
            _UnitofWork.Save();
            return Json(new { success = true, message = "Data deleted success fully" });
        }
        #endregion
    }
}
