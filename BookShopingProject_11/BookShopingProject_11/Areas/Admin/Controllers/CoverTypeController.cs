using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShoping_Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopingProject_11.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles =SD.Role_Admin)]
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
            param.Add("@Id", id.GetValueOrDefault());

            //coverType = _unitofWork.coverType.Get(id.GetValueOrDefault());

            coverType = _unitofWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_GetCoverType, param);
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
            if (!ModelState.IsValid)
                return View(coverType);

            var param = new DynamicParameters();
            param.Add("@Name", coverType.Name);
            if (coverType.Id == 0)

                //_unitofWork.coverType.Add(coverType);

                _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Create, param);
            else
            {
                param.Add("@Id", coverType.Id);
                _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Update, param);
            }
            //_unitofWork.coverType.update(coverType);
            //_unitofWork.save();


            return RedirectToAction(nameof(Index));
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            //var CoverType = _unitofWork.coverType.GetAll();

            var CoverType = _unitofWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetCoverTypes);

            return Json(new { data = CoverType });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CoverTypeInDb = _unitofWork.coverType.Get(id);
            if (CoverTypeInDb == null)
                return Json(new { Success = false, message = "Error While delete data" });
            var param = new DynamicParameters();
            param.Add("@Id", id);
            _unitofWork.SP_Call.Execute(SD.Proc_CoverType_Delete, param);


            //_unitofWork.coverType.Remove(CoverTypeInDb);
            //_unitofWork.save();

            return Json(new { Success = true, message = "Data successfully deleted!!!" });
        }

        #endregion
    }
}
