using BookShopping.DataAccess.Repository.IRepository;
using BookShopping.Models;
using BookShopping.Models.ViewModels;
using BookShopping.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject.MVC.CORE.Understand2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList=_unitofWork.category.GetAll().Select(cl=>new SelectListItem() 
                { 
                  Text=cl.Name,
                  Value=cl.Id.ToString()
                }),
                CoverTypeList=_unitofWork.coverType.GetAll().Select(ct=>new SelectListItem()
                {
                    Text=ct.Name,
                    Value=ct.Id.ToString()
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
            if(ModelState.IsValid)
            {
                var webrootPath = _webHostEnviroment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(files.Count>0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webrootPath, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);
                    if(productVM.Product.Id != 0)
                    {
                        var imageExist = _unitofWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageExist;
                    }
                    if(productVM.Product.ImageUrl !=null)
                    {
                        var imagePath = Path.Combine(webrootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                else
                {
                    if(productVM.Product.Id!=0)
                    {
                        var imageExist = _unitofWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageExist;
                    }
                }
                if (productVM.Product.Id == 0)
                    _unitofWork.Product.Add(productVM.Product);
                else
                    _unitofWork.Product.Update(productVM.Product);
                _unitofWork.save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM = new ProductVM()
                {
                    CategoryList = _unitofWork.category.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString()
                    }),
                    CoverTypeList=_unitofWork.coverType.GetAll().Select(ct=>new SelectListItem() 
                    {
                      Text=ct.Name,
                      Value=ct.Id.ToString()
                    })
                };

            }
            if(productVM.Product.Id!=0)
            {
                productVM.Product = _unitofWork.Product.Get(productVM.Product.Id);
            }
            return View(productVM);
        }

        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var productInDb = _unitofWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productInDb });

        }

        [HttpDelete]
        
        public IActionResult Delete(int id)
        {
            var productInDb = _unitofWork.Product.Get(id);
            if (productInDb == null)
                return Json(new { success = false, message = "error while deleting data" });
            if(productInDb.Id !=0)
            {
                var webrootPath = _webHostEnviroment.WebRootPath;
                var filePath = Path.Combine(webrootPath, productInDb.ImageUrl.TrimStart('\\'));
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                
            }
            _unitofWork.Product.Remove(productInDb);
            _unitofWork.save();
            return Json(new { success = true, message = "data deleted successfully" });
        }
        #endregion
    }
}
