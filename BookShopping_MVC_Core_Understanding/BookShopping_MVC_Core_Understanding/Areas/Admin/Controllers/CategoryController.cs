using Bookshopping.DataAccess.Repository.IRepository;
using Bookshopping.Models;
using Bookshopping.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping_MVC_Core_Understanding.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=SD.Role_Admin)]
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
            var categoryInDb = _unitofWork.category.Get(id.GetValueOrDefault());
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
            var categoryList = _unitofWork.category.GetAll();
            return Json(new { data = categoryList });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CategoryInDb = _unitofWork.category.Get(id);
            if (CategoryInDb == null)
                return Json(new { success = false, message = "Error While Deleting Data" });
            else
                _unitofWork.category.Remove(CategoryInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data deleted successfully" });
        }
        #endregion
    }
}
