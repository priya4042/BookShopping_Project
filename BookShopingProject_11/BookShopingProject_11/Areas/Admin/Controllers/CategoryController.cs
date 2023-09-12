using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShoping_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopingProject_11.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        //[AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Descri(int? id)
        {
            Category category = new Category();
            if (id == null)
                return View(category);
            var CategoryInDb = _unitofWork.Category.Get(id.GetValueOrDefault());
            if (CategoryInDb == null)
                return NotFound();
            return View(CategoryInDb);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Descri(Category category)
        {
            if (category == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(category);

            if (category.Id == 0)
                _unitofWork.Category.Add(category);
            else
                _unitofWork.Category.update(category);
            _unitofWork.save();
            return RedirectToAction(nameof(Index));



        }


        #region APIs
        [HttpGet]
        //[AllowAnonymous] with this this getall access it
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
                return Json(new { Success = false, message = "Want to Delete the data" });
            _unitofWork.Category.Remove(CategoryInDb);
            _unitofWork.save();
            return Json(new { Success = true, message = "Data successfully delete!!!" });
        }


        #endregion
    }
}