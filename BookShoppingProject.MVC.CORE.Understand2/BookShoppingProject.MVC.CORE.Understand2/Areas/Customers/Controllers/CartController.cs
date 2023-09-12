using BookShopping.DataAccess.Repository.IRepository;
using BookShopping.Models;
using BookShopping.Models.ViewModels;
using BookShopping.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShoppingProject.MVC.CORE.Understand2.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class CartController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CartController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                ShoppingCartVM = new ShoppingCartVM()
                {
                    ListCart = new List<ShoppingCart>()
                };
                return View(ShoppingCartVM);
            }
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _unitofWork.shoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product")
            };

            ShoppingCartVM.OrderHeader.OrderTotal = 0;
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitofWork.Application.FirstOrDefault(u => u.Id == claim.Value, includeProperties: "Company");
            foreach (var list in ShoppingCartVM.ListCart)

            {
                list.Price = SD.GetPriceBasedOnQuantity(list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (list.Count * list.Price);

                if (list.Product.Description.Length > 100)
                {
                    list.Product.Description = list.Product.Description.Substring(0, 99) + ".....";
                }

            }

            return View(ShoppingCartVM);
        }
        public IActionResult plus(int cartId)
        {
            var cart = _unitofWork.shoppingCart.FirstOrDefault(sc => sc.Id == cartId, includeProperties: "Product");
            cart.Count += 1;
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult minus(int cartId)
        {
            var cart = _unitofWork.shoppingCart.FirstOrDefault(sc => sc.Id == cartId, includeProperties: "Product");
            cart.Count -= 1;
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult remove(int cartId)
        {
            var cart = _unitofWork.shoppingCart.FirstOrDefault(sc => sc.Id == cartId, includeProperties: "Product");
            _unitofWork.shoppingCart.Remove(cart);
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }

        //   
        public IActionResult summary()
        {
            var claimIdentity = (ClaimsIdentity)(User.Identity);
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _unitofWork.shoppingCart.GetAll
                (u => u.ApplicationUserId == claim.Value, includeProperties: "Product")

            };
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitofWork.Application.FirstOrDefault
                (u => u.Id == claim.Value, includeProperties: "Company");
            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity(list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (list.Price * list.Count);
                list.Product.Description = SD.ConvertToRowHtml(list.Product.Description);
            }
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

            return View(ShoppingCartVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("summary")]
        public IActionResult summaryPost(string stripeToken)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitofWork.Application.
                FirstOrDefault(u => u.Id == claim.Value, includeProperties: "Company");

            ShoppingCartVM.ListCart = _unitofWork.shoppingCart.GetAll
                (sc => sc.ApplicationUserId == claim.Value, includeProperties: "Product");

            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusPending;
            ShoppingCartVM.OrderHeader.Orderdate = DateTime.Now;
            ShoppingCartVM.OrderHeader.AppliationUserId = claim.Value;

            _unitofWork.orderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitofWork.save();

            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity
                    (list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);

                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductId = list.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = list.Price,
                    Count = list.Count
                };

                ShoppingCartVM.OrderHeader.OrderTotal += (orderDetails.Price * orderDetails.Count);
                _unitofWork.orderdetail.Add(orderDetails);
                //  _unitofWork.save();


            }

            _unitofWork.shoppingCart.RemoveRange(ShoppingCartVM.ListCart);
            _unitofWork.save();
            HttpContext.Session.SetInt32(SD.Ss_Session, 0);

            #region stripe Payment

            if (stripeToken == null)
            {
                ShoppingCartVM.OrderHeader.PaymentDueDate = DateTime.Now.AddDays(30);

                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayPayment;

                ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusApproved;
            }
            else
            {
                var options = new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(ShoppingCartVM.OrderHeader.OrderTotal),

                    Currency = "inr",

                    Description = "order Id:" + ShoppingCartVM.OrderHeader.Id,

                    Source = stripeToken
                };
                var service = new ChargeService();

                Charge charge = service.Create(options);

                if (charge.BalanceTransactionId == null)
                    ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;

                else
                    //
                    ShoppingCartVM.OrderHeader.TransactionId = charge.BalanceTransactionId;
                if (charge.Status.ToLower() == "Succeded")
                {
                    ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;

                    ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusApproved;

                    ShoppingCartVM.OrderHeader.PaymentDate = DateTime.Now;
                }
            }
            _unitofWork.save();

            #endregion

            return RedirectToAction("OrderConfirmation", "cart", new { Id = ShoppingCartVM.OrderHeader.Id });

        }

        public IActionResult OrderConfirmation(int? id)
        {
            return View(id);
        }


    }
}
