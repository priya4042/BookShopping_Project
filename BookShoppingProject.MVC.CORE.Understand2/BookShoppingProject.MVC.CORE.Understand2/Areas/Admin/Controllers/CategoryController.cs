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
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofwork = unitofWork;
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
            var CategoryInDb = _unitofwork.category.Get(id.GetValueOrDefault());
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
                _unitofwork.category.Add(category);
            else
                _unitofwork.category.Update(category);
            _unitofwork.save();
            return RedirectToAction(nameof(Index));
        }
        #region APIs

        public IActionResult GetAll()
        {
            var CategoryList = _unitofwork.category.GetAll();
            return Json(new { data = CategoryList });
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CategoryIndb = _unitofwork.category.Get(id);
            if (CategoryIndb == null)
                return Json(new { success = false, message = "Error While Deleting Data" });
            _unitofwork.category.Remove(CategoryIndb);
            _unitofwork.save();
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }
        #endregion
    }
}
