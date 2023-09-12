using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using BookShoppingProject.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitWork _unitofWork;
        public CoverTypeController(IUnitWork unitWork)
        {
            _unitofWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverTypes = new CoverType();
            if (id == null)
                return View(coverTypes);
            var param = new DynamicParameters();
            param.Add("@Id", id.GetValueOrDefault());
            coverTypes = _unitofWork.SP_Call.OneRecord<CoverType>(SD.Proc_GetCoverType, param);

            // var coverTypes = _unitofWork.coverType.Get(id.GetValueOrDefault());

            if (coverTypes == null)
                return NotFound();
            return View(coverTypes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverTypes)
          {
            if (coverTypes == null)
                return NotFound();
            if (!ModelState.IsValid)
                return View(coverTypes);
            var param = new DynamicParameters();
            param.Add("@name", coverTypes.Name);
            if (coverTypes.Id == 0)
                //_unitofWork.coverType.Add(coverTypes);
                _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Create, param);
            else
            {
                param.Add("@Id", coverTypes.Id);
                _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Update, param);
            }

            //_unitofWork.coverType.Update(coverTypes);
            //_unitofWork.save();
            return RedirectToAction(nameof(Index));
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            //var CoverTypeList = _unitofWork.coverType.GetAll();
            var CoverTypeList = _unitofWork.SP_Call.List<CoverType>(SD.Proc_GetCoverTypes);
            return Json(new { data = CoverTypeList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CoverTypeInDb = _unitofWork.coverType.Get(id);
            if(CoverTypeInDb == null)
                return Json(new { success = false, message = "Error while deleting data" });
            //_unitofWork.coverType.Remove(CoverTypeInDb);
            var param = new DynamicParameters();
            param.Add("@Id", id);
            //_unitofWork.save();
            _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Delete, param);
            
            return Json(new { success = true, message = "Data deleted successfully" });


        }
        #endregion
    }
}
