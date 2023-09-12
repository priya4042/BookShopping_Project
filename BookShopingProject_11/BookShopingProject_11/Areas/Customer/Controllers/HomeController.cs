using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShoping_Utility;
using BookShopingProject.Data_Access.Data;
using BookShopingProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShopingProject_11.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        
        private readonly ILogger<HomeController> _logger;

       

        public HomeController(ILogger<HomeController> logger,IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
            
        }

        public IActionResult Index()
        {
            var productList = _unitofWork.Product.GetAll(includeProperties: "Category,CoverType");

            //session

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null)
            {
                var count = _unitofWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_Session, count);
            }
            return View(productList);
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Details(int id)
        {
            var ProductInDb = _unitofWork.Product.FirstOrDefault(p => p.Id == id, includeProperties: "Category,CoverType");
            if (ProductInDb == null)
                return NotFound();
            var ShoppingCart = new ShoppingCart()
            {
                Product = ProductInDb,
                ProductId = ProductInDb.Id
            };
            return View(ShoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCartobj)
        {
            shoppingCartobj.Id = 0;
            if(ModelState.IsValid)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

                shoppingCartobj.ApplicationUserId = claim.Value;

                var shoppingCartfromDb = _unitofWork.ShoppingCart.FirstOrDefault
                    (u => u.ApplicationUserId == claim.Value && u.Product.Id == shoppingCartobj.ProductId);
                if(shoppingCartfromDb == null)
                {
                    // Add to Cart
                    _unitofWork.ShoppingCart.Add(shoppingCartobj);
                }
                else
                {
                    shoppingCartfromDb.Count += shoppingCartobj.Count;
                }
                _unitofWork.save();

                //session

                var count = _unitofWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_Session, count);
                return RedirectToAction("Index");
            }
            else
            {
                var productInDb = _unitofWork.Product.FirstOrDefault
                    (p => p.Id == shoppingCartobj.ProductId, includeProperties: "Category,CoveType");
                var shoppingCart = new ShoppingCart()
                {
                    Product = productInDb,
                    ProductId = productInDb.Id
                };
                return View(shoppingCart);

            }
        }
    }
}
