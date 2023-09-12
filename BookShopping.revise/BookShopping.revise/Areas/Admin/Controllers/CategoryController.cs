using BookShopping_DataAccess.Repository.IRepository;
using BookShopping_Models;
using BookShopping_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.revise.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IunitofWork _unitofWork;
        public CategoryController(IunitofWork unitofWork)
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
            category = _unitofWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
                return NotFound(category);
            return View(category);
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

                _unitofWork.Category.Add(category);
            else
                _unitofWork.Category.Update(category);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));

        }

        #region categoryregion
        [HttpGet]

        public IActionResult GetAll()
        {
            var categoryInDb = _unitofWork.Category.GetAll();
            return Json(new { data = categoryInDb });
        }
 [HttpDelete]

        public IActionResult Delete(int id)
        {
            var categoryfromdb = _unitofWork.Category.Get(id);
            if (categoryfromdb == null)
                return Json(new { success = false, message = "error while deleteing data" });
            _unitofWork.Category.Remove(categoryfromdb);
            _unitofWork.Save();
            return Json(new { success = true, message = "data deleted successfully" });
        }
        #endregion
    }
}
