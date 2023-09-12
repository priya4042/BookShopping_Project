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
    [Authorize(Roles =SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public CoverTypeController(IUnitofWork unitofWork)
        {
            _unitofwork = unitofWork;
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
            var CoverTypeInDb = _unitofwork.CoverType.Get(id.GetValueOrDefault());
            if (CoverTypeInDb == null)
                return NotFound();
            return View(CoverTypeInDb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null)
                return View();
            if (!ModelState.IsValid)
                return View(coverType);
            if (coverType.Id == 0)
                _unitofwork.CoverType.Add(coverType);
            else
                _unitofwork.CoverType.Update(coverType);
            _unitofwork.Save();
            return RedirectToAction(nameof(Index));
        }

        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var coverTypeList = _unitofwork.CoverType.GetAll();
            return Json(new { data = coverTypeList });
        }

      [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CoverTypeInDb = _unitofwork.CoverType.Get(id);
            if (CoverTypeInDb == null)
                return Json(new { success = false, message = "Error While Deleting Data" });
            else
                _unitofwork.CoverType.Remove(CoverTypeInDb);
            _unitofwork.Save();
            return Json(new { success = true, message = "Error While Deleting Data" });
        }
        #endregion
    }
}
