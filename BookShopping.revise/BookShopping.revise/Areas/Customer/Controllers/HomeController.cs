using BookShopping.revise.Data;
using BookShopping_DataAccess.Repository.IRepository;
using BookShopping_Models;
using BookShopping_Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShopping.revise.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IunitofWork _unitofWork;

        public HomeController(ILogger<HomeController> logger,IunitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            var productfromdb = _unitofWork.Product.GetAll(includeProperties: "Category,coverType");
            return View(productfromdb);
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

        public  IActionResult Details(int id)
        {
            var productInDb = _unitofWork.Product.FirstOrDefault(p => p.Id == id, includeProperties: "Category,coverType");
            if (productInDb == null)
                return NotFound();
            var shoppingcart = new ShoppingCarts()
            {
                Product = productInDb,
                ProductId = productInDb.Id
            };
            return View(shoppingcart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public IActionResult Details(ShoppingCarts shoppingCartobj)
        {
            shoppingCartobj.Id = 0;
            if(ModelState.IsValid)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

                shoppingCartobj.ApplicationUserId = claim.Value;

                var shoppingCartIndb = _unitofWork.ShoppingCart.FirstOrDefault
                    (u => u.ApplicationUserId == claim.Value && u.Product.Id == shoppingCartobj.ProductId);
                if(shoppingCartIndb==null)
                {
                    _unitofWork.ShoppingCart.Add(shoppingCartobj);
                }
                else
                {
                    shoppingCartIndb.Count = shoppingCartobj.Count;
                }
                _unitofWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                var productIndb = _unitofWork.Product.FirstOrDefault
                    (p => p.Id == shoppingCartobj.ProductId,includeProperties: "Category,coverType");
                var shoppingCart = new ShoppingCarts()
                {
                    Product = productIndb,
                    ProductId = productIndb.Id
                };
                return View(shoppingCart);
            }

        }


    }
}
