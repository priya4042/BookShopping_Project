using BookShoppingProject.Models;
using BookShoppingProject.Utility;
using BookShoppingProject_MVC_CORE_UnderStanding3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_MVC_CORE_UnderStanding3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
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
            var userList = _context.ApplicatiionUsers.Include(ul => ul.Company).ToList();
            var roles = _context.Roles.ToList();
            var userRoles = _context.UserRoles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(rl => rl.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(ur => ur.Id == roleId).Name;
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }
            if (!User.IsInRole(SD.Role_Admin))
            {
                var adminuser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            }
            return Json(new { data = userList });
        }

        [HttpPost]

        public IActionResult LockUnLock([FromBody] string id)
        {
            bool isLocked = false;
            var userIndb = _context.ApplicatiionUsers.FirstOrDefault(cl => cl.Id == id);
            if (userIndb == null)

                return Json(new { success = false, message = "Error while Locking UnLocking User" });
            if (userIndb != null && userIndb.LockoutEnd > DateTime.Now)
            {
                userIndb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userIndb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new
            {
                success = true,
                message = isLocked == true ? "user successfully Locked" : "user successfully unlocked"
            });
        }
        #endregion
    }
}
