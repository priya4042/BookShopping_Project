using BookShoppingProject.DataAccess.Repository.IRepository;
using BookShoppingProject.Models;
using BookShoppingProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_MVC_CORE_UnderStanding3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public ProductController(IUnitofWork unitofWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = unitofWork;
            _webHostEnviroment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitofWork.Category.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.Id.ToString()
                }),
                CoverTypeList = _unitofWork.CoverType.GetAll().Select(ct => new SelectListItem()
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                })

            };
            if (id == null)
                return View(productVM);
            productVM.Product = _unitofWork.Product.Get(id.GetValueOrDefault());
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var webrootPath = _webHostEnviroment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var filesName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webrootPath, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (productVM.Product.Id != 0)
                    {
                        var imageExist = _unitofWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageExist;
                    }
                    if (productVM.Product.ImageUrl != null)
                    {
                        var filePath = Path.Combine(webrootPath, productVM.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, filesName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + filesName + extension;
                }
                else
                {
                    if (productVM.Product.Id != 0)
                    {
                        var imageExists = _unitofWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageExists;
                    }
                }
                if (productVM.Product.Id == 0)
                    _unitofWork.Product.Add(productVM.Product);
                else
                    _unitofWork.Product.update(productVM.Product);
                _unitofWork.Save();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                productVM = new ProductVM()
                {
                    CategoryList = _unitofWork.Category.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString()
                    }),
                    CoverTypeList = _unitofWork.CoverType.GetAll().Select(ct => new SelectListItem()
                    {
                        Text = ct.Name,
                        Value = ct.Id.ToString()
                    })

                };
                if (productVM.Product.Id != 0)
                {
                    _unitofWork.Product.Get(productVM.Product.Id);
                }
                return View(productVM);
            }
        }

        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var ProductInDb = _unitofWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = ProductInDb });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productfromdb = _unitofWork.Product.Get(id);
            if (productfromdb == null)
                return Json(new { success = false, message = "Error while deletion data" });
            if (productfromdb.ImageUrl != "")
            {
                var webrootPath = _webHostEnviroment.WebRootPath;
                var imagePath = Path.Combine(webrootPath, productfromdb.ImageUrl.Trim('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _unitofWork.Product.Remove(productfromdb);
            _unitofWork.Save();
            return Json(new { success = true, message = "data deleted successfully" });
        }

        #endregion
    }
}
