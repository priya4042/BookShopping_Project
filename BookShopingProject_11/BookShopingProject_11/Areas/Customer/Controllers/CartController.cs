using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Models;
using BookShoping_Models.ViewModels;
using BookShoping_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BookShopingProject_11.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private static bool isEmailConfirm = false;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;
        public CartController(IUnitofWork unitofWork,IEmailSender emailSender,UserManager<IdentityUser> userManager)
        {
            _unitofWork = unitofWork;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim == null)
            {
                ShoppingCartVM = new ShoppingCartVM()
                {
                    ListCart=new List<ShoppingCart>()
                };
                return View(ShoppingCartVM);
            }
            //When User Login
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _unitofWork.ShoppingCart.
                GetAll(u=>u.ApplicationUserId == claim.Value, includeProperties:"Product")
            };

            ShoppingCartVM.OrderHeader.OrderTotal = 0;

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitofWork.ApplicationUser.
                FirstOrDefault(u => u.Id == claim.Value, includeProperties: "Company");

            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity(list.Count, 
                    list.Product.Price, list.Product.Price50, list.Product.Price100);

                ShoppingCartVM.OrderHeader.OrderTotal += (list.Count * list.Price);
                if(list.Product.Discription.Length>100)
                {
                    list.Product.Discription = list.Product.Discription.Substring(0, 99) + "...";
                }
            }
            if(isEmailConfirm)
            {
                ViewBag.EmailMessage = "Email has been send kindly verify your email";
                ViewBag.EmailCSS = "text-success";
                isEmailConfirm = false;
            }
            else
            {
                ViewBag.EmailMessage = "Email must be confirm for authorize customer!!";
                ViewBag.EmailCSS = "text-danger";
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public async Task<IActionResult>Indexpost()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitofWork.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value);
            if(user==null)
            {
                ModelState.AddModelError(string.Empty, "Email is empty");
            }
            else
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                ModelState.AddModelError("", "verification mail send Please confirm");
                isEmailConfirm = true;
            }
            return RedirectToAction("Index");
        }

        public IActionResult plus(int cartId)
        {
            var cart = _unitofWork.ShoppingCart.FirstOrDefault(sc => sc.Id == cartId, includeProperties: "Product");
            cart.Count += 1;
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult minus(int cartId)
        {
            var cart = _unitofWork.ShoppingCart.FirstOrDefault(sc => sc.Id == cartId, includeProperties: "Product");
            cart.Count -= 1;
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult remove(int cartId)
        {
            var cart = _unitofWork.ShoppingCart.FirstOrDefault(sc => sc.Id == cartId, includeProperties: "Product");
            _unitofWork.ShoppingCart.Remove(cart);
            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult summary()
        {
            var claimIdentity = (ClaimsIdentity)(User.Identity);
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart=_unitofWork.ShoppingCart.GetAll
                (u=>u.ApplicationUserId==claim.Value,includeProperties:"Product")

            };
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitofWork.ApplicationUser.FirstOrDefault
                (u => u.Id == claim.Value, includeProperties: "Company");
            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity(list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (list.Price * list.Count);
                list.Product.Discription = SD.ConvertToRowHtml(list.Product.Discription);
            }
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
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
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitofWork.ApplicationUser.
                FirstOrDefault(u => u.Id == claim.Value, includeProperties: "Company");

            ShoppingCartVM.ListCart = _unitofWork.ShoppingCart.GetAll
                (sc => sc.ApplicationUserId == claim.Value, includeProperties: "Product");

            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusPending;
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;

            _unitofWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitofWork.save();

            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity
                    (list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);

                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductId=list.ProductId,
                    OrderId=ShoppingCartVM.OrderHeader.Id,
                    Price=list.Price,
                    Count=list.Count
                };

                ShoppingCartVM.OrderHeader.OrderTotal += (orderDetails.Price * orderDetails.Count);
                _unitofWork.OrderDetails.Add(orderDetails);
              //  _unitofWork.save();


            }

            _unitofWork.ShoppingCart.RemoveRange(ShoppingCartVM.ListCart);
            _unitofWork.save();
            HttpContext.Session.SetInt32(SD.Ss_Session, 0);

            #region stripe Payment

            if(stripeToken == null)
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
                    ShoppingCartVM.OrderHeader.TransactionId = charge.BalanceTransactionId;
                if(charge.Status.ToLower()=="Succeded")
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
