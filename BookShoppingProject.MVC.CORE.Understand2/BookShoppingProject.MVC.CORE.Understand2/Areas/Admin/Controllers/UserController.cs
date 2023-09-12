using BookShopping.Models;
using BookShopping.Utility;
using BookShoppingProject.MVC.CORE.Understand2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject.MVC.CORE.Understand2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs

        [HttpGet]

        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.Include(cl => cl.Company).ToList();
            var role = _context.Roles.ToList();
            var userRole = _context.UserRoles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(cl => cl.UserId == user.Id).RoleId;
                user.Role = role.FirstOrDefault(rl => rl.Id == roleId).Name;
                if(user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };

                }
            }
            if(!User.IsInRole(SD.Role_Admin))
            {
                var adminuser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
                userList.Remove(adminuser);
            }
            return Json(new { data = userList });
        }

        [HttpPost]

        public IActionResult LockUnLock([FromBody] string id)
        {
            bool islocked = false;
            var userIndb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (userIndb == null)
                return Json(new { success = false, message = "error while locking and unlocking data" });
            if(userIndb != null && userIndb.LockoutEnd > DateTime.Now)
            {
                userIndb.LockoutEnd = DateTime.Now;
                islocked = false;
            }
            else
            {
                userIndb.LockoutEnd = DateTime.Now.AddYears(100);
                islocked = true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = islocked == true ? "successfully locked" : "Successfully UnLocked" });
        }

        #endregion
    }
}
