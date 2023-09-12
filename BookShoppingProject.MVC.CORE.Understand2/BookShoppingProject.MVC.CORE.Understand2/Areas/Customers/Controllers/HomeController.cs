using BookShopping.DataAccess.Repository.IRepository;
using BookShopping.Models;
using BookShopping.Models.ViewModels;
using BookShopping.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShoppingProject.MVC.CORE.Understand2.Controllers
{
    [Area("Customers")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;
        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            var Productlist = _unitofWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(Productlist);
            //session

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitofWork.shoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_Session, count);
            }
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
            var productinDb = _unitofWork.Product.FirstOrDefault(s => s.Id == id, includeProperties: "Category,CoverType");
            if (productinDb == null)
                return NotFound();
            var shoppingcart = new ShoppingCart()
            {
                Product = productinDb,
                ProductId = productinDb.Id
            };
            return View(shoppingcart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCartobj)
        {
            shoppingCartobj.Id = 0;
            if (ModelState.IsValid)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

                shoppingCartobj.ApplicationUserId = claim.Value;

                var shoppingCartfromDb = _unitofWork.shoppingCart.FirstOrDefault
                    (u => u.ApplicationUserId == claim.Value && u.Product.Id == shoppingCartobj.ProductId);
                if (shoppingCartfromDb == null)
                {
                    // Add to Cart
                    _unitofWork.shoppingCart.Add(shoppingCartobj);
                }
                else
                {
                    shoppingCartfromDb.Count += shoppingCartobj.Count;
                }
                _unitofWork.save();

                //session

                var count = _unitofWork.shoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
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

