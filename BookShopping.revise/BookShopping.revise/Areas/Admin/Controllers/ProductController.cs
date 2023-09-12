using BookShopping_DataAccess.Repository.IRepository;
using BookShopping_Models;
using BookShopping_Models.ViewModels;
using BookShopping_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping.revise.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IunitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public ProductController(IunitofWork unitofWork,IWebHostEnvironment webHostEnvironment)
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
                CategoryList =_unitofWork.Category.GetAll().Select(cl=>new SelectListItem()
                {
                    Text = cl.Name,
                   Value  =cl.Id.ToString()
                }),
                CoverTypeList=_unitofWork.CoverType.GetAll().Select(cl=>new SelectListItem()
                {
                    Text=cl.Name,
                    Value=cl.Id.ToString()
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
                var webhostroot = _webHostEnviroment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webhostroot, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (productVM.Product.Id != 0)
                    {
                        var imagesExist = _unitofWork.Product.Get(productVM.Product.Id).ImageURl;
                        productVM.Product.ImageURl = imagesExist;
                    }
                    if (productVM.Product.ImageURl != null)
                    {
                        var filePtah = Path.Combine(webhostroot, productVM.Product.ImageURl.TrimStart('\\'));
                        if (System.IO.File.Exists(filePtah))
                        {
                            System.IO.File.Delete(filePtah);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    productVM.Product.ImageURl = @"\images\products\" + fileName + extension;
                }
                else
                {
                    if (productVM.Product.Id != 0)
                    {
                        var imageExist = _unitofWork.Product.Get(productVM.Product.Id).ImageURl;
                        productVM.Product.ImageURl = imageExist;
                    }
                }
                if (productVM.Product.Id == 0)
                    _unitofWork.Product.Add(productVM.Product);
                else
                    _unitofWork.Product.Update(productVM.Product);
                _unitofWork.Save();
                return RedirectToAction("Index");
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
                    CoverTypeList = _unitofWork.CoverType.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString()
                    })
                };

                if (productVM.Product.Id != 0)
                {
                    productVM.Product = _unitofWork.Product.Get(productVM.Product.Id);
                }
                return View(productVM);
            }
        }
        
        #region productRegion

        [HttpGet]
        public IActionResult GetAll()
        {
            var productInDb = _unitofWork.Product.GetAll(includeProperties: "Category,coverType");
            return Json(new { data = productInDb });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productfromdb = _unitofWork.Product.Get(id);
            if (productfromdb == null)
                return Json(new { success = false, message = "error while Deleting data" });
            if(productfromdb.ImageURl =="")
            {
                var webRootpath = _webHostEnviroment.WebRootPath;
                var imagePath = Path.Combine(webRootpath, productfromdb.ImageURl.TrimStart('\\'));
                if(System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                
                

            }
            _unitofWork.Product.Remove(productfromdb);
            _unitofWork.Save();
            return Json(new { success = true, message = "Data deleted successfully" });
        }

        #endregion
    }
}
