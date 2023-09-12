using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShoping_Models.ViewModels;
using BookShoping_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopingProject_11.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles =SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
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
                CoverTypeList = _unitofWork.coverType.GetAll().Select(ct => new SelectListItem()
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
                var WebRoothPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(WebRoothPath, @"Image\Product");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (productVM.Product.Id != 0)
                    {
                        var ImageExists = _unitofWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = ImageExists;
                    }
                    if (productVM.Product.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(WebRoothPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filestream = new FileStream
                        (Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    productVM.Product.ImageUrl = @"\Image\Product\" + fileName + extension;
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
                    _unitofWork.Product.Update(productVM.Product);
                _unitofWork.save();
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
                    CoverTypeList = _unitofWork.coverType.GetAll().Select(ct => new SelectListItem()
                    {
                        Text=ct.Name,
                        Value=ct.Id.ToString()
                    })
                 };
                if(productVM.Product.Id != 0)
                {
                    productVM.Product = _unitofWork.Product.Get(productVM.Product.Id);
                }
                return View(productVM);
            }
        }






        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {

            var productList = _unitofWork.Product.GetAll(includeProperties: "Category,CoverType");
            //foreach (var item in productList)
            //{
            //    item.Author = item.Author + " " + item.ISBN;
            //}
            return Json(new { data = productList });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var productInDb = _unitofWork.Product.Get(id);
            if (productInDb == null)
                return Json(new { success = false, message = "error while deleting data" });
            if(productInDb.ImageUrl != "")
            {
                var WebRootPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(WebRootPath, productInDb.ImageUrl.TrimStart('\\'));
                if(System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _unitofWork.Product.Remove(productInDb);
            _unitofWork.save();
            return Json(new { success = true, message = "data deleted Successfully!!!" });
        }
        #endregion
    }
}
