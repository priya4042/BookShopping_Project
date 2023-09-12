using BookShopingProject.DataAccess.Repository.IRepository;
using BookShopingProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_CoverType_StoreProcedure.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitodWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitodWork = unitofWork;
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
            var CategoryInDb = _unitodWork.Category.Get(id.GetValueOrDefault());
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
               _unitodWork.Category.Add(category);
            else
                _unitodWork.Category.Update(category);
            _unitodWork.Save();
            return RedirectToAction(nameof(Index));
             }


        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var CategoryList = _unitodWork.Category.GetAll();
            return Json(new { data = CategoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CategoryFromDb = _unitodWork.Category.Get(id);
            if (CategoryFromDb == null)
                return Json(new { success = false, message = "Error While Deleting Data" });
            _unitodWork.Category.Remove(CategoryFromDb);
            _unitodWork.Save();
            return Json(new { success = true, message = "Data Deleted successfully" });
        }
        #endregion
    }
}
