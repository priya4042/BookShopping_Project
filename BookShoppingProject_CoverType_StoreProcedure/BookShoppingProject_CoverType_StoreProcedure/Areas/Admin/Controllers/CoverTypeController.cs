using BookShopingProject.DataAccess.Repository.IRepository;
using BookShopingProject.Models;
using BookShopingProject.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_CoverType_StoreProcedure.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CoverTypeController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
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
            var param = new DynamicParameters();
            param.Add("@Id", coverType.Id);
            if (coverType == null)
                return NotFound();
            return View(coverType);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null)
                return NotFound();
            if (ModelState.IsValid)
                return View(coverType);
            var param = new DynamicParameters();
            param.Add("@Name", coverType.Name);
            if (coverType.Id == 0)
                _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Create, param);
            else
            {
                param.Add("@Id", coverType.Id);
                _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Update, param);
            }
            return RedirectToAction(nameof(Index));
        }

        #region APLs

        [HttpGet]

        public IActionResult GetAll()
        {
            var CoverTypeList = _unitofWork.SP_Call.List<CoverType>(SD.Proc_CoverTypes);

            return Json(new { data = CoverTypeList });
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var CoverTypefromDb = _unitofWork.CoverType.Get(id);
            if (CoverTypefromDb == null)
                return Json(new { success = false, message = "Error While Deleting Data" });
            var param = new DynamicParameters();
            param.Add("@Id", id);
            _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Delete, param);
            return Json(new { success = true, message = "Data Deleted Successfully" });

        }
        #endregion
    }
}
