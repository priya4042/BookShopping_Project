using BookShoping_Models;
using BookShoping_Utility;
using BookShopingProject.Data_Access.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopingProject_11.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin +","+SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var userlist = _context.ApplicationUsers.Include(c => c.Company).ToList();
            var roles = _context.Roles.ToList();
            var userRoles = _context.UserRoles.ToList();

            foreach(var user in userlist)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if(user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };

                }
               
            }
            if (User.IsInRole(SD.Role_Admin))
            {
                var AdminUser = userlist.FirstOrDefault(u => u.Role == SD.Role_Admin);
                userlist.Remove(AdminUser);
            }

            return Json(new { data = userlist });
        }
        [HttpPost]

        public IActionResult LockUnLock([FromBody] string id)
        {
            bool isLocked = false;
            var userInDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (userInDb == null)
                return Json(new { success = false, message = "Error while Locking and UnLocking Data" });
            //if (userInDb.Role == SD.Role_Admin)
            //    return Json(new { success = false, message = "You not Allowed" });
            if(userInDb != null && userInDb.LockoutEnd>DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = isLocked == true ? "User Successfully Locked" : "User Successfully Unlocked" });
        }
        #endregion
    }
}
