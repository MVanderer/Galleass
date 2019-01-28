using System;
using System.Collections.Generic;
using System.Linq;
using Galleass.Data;
using Galleass.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Galleass.Controllers
{
    public class AdminController : Controller
    {
        private DataContext dbContext;
        public AdminController (DataContext context) 
        {
            dbContext = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Landing()
        {
            return View();
        }
        [HttpGet("loginreg")]
        public IActionResult LoginReg()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(newUser.Email == "adion81@gmail.com" || newUser.Email == "vfmadmax@gmail.com")
                {
                    if(dbContext.Users.Any(u => u.Email == newUser.Email))
                    {
                        ModelState.AddModelError("Email", "Email already in use!");
                    }
                    PasswordHasher<User> AdminHasher = new PasswordHasher<User>();
                    newUser.Password = AdminHasher.HashPassword(newUser, newUser.Password);
                    newUser.Admin = true;
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("UserId",newUser.UserId);
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                newUser.Admin = false;
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId",newUser.UserId);
                return RedirectToAction("Index", "Home");
            }
            return View("LoginReg");
        }
        [HttpPost("login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email");
                    return View("LoginReg","Admin");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password,userSubmission.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "That isn't the correct password for this email address!");
                    return View("LoginReg","Admin");
                }
                if(userInDb.Admin == true)
                {
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("AdminDashboard","Admin");
                }
                if(userInDb.Admin == false)
                {
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("Index","Home");
                }
            }
            return View("LoginReg","Admin");
        }
        [HttpGet("/admindashboard")]
        public IActionResult AdminDashboard()
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg","Admin");
            }
            int? id = HttpContext.Session.GetInt32("UserId");
            User userInDb = dbContext.Users.FirstOrDefault(u => u.UserId == id);
            if(userInDb.Admin == false)
            {
                return RedirectToAction("LoginReg","Admin");
            }
            ViewBag.Admin = userInDb.FirstName +" " + userInDb.LastName;
            return View();
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Landing", "Admin");
        }
    }
}