using System;
using System.Collections.Generic;
using System.Linq;
using Galleass.Data;
using Galleass.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

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
                        return View("LoginReg");
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
                    return View("LoginReg");
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
                    return View("LoginReg");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password,userSubmission.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "That isn't the correct password for this email address!");
                    return View("LoginReg");
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
            return View("LoginReg");
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
            List<Port> allPorts = dbContext.Ports.ToList();
            List<VesselType> allShips = dbContext.VesselTypes.ToList();
            List<TradeGood> allTradeGoods = dbContext.TradeGoods.ToList();
            ViewBag.Ports = allPorts;
            ViewBag.Ships = allShips;
            ViewBag.TradeGoods = allTradeGoods;
            ViewBag.Admin = userInDb.FirstName + " " + userInDb.LastName;
            List<GridSquare> gridsToMap = dbContext.GridSquares.ToList();
            return View();
        }
        [HttpPost("mapsize")]
        public IActionResult MapSize (int mapSizeY, int mapSizeX)
        {
            List<GridSquare> allGrids = dbContext.GridSquares.ToList();
            foreach(var g in allGrids)
            {
                dbContext.GridSquares.Remove(g);
            }
            for(int y = 0; y < mapSizeY; y++)
            {
                for(int x = 0; x < mapSizeX; x++)
                {
                    GridSquare newGridSquare = new GridSquare();
                    newGridSquare.xCoord = x;
                    newGridSquare.yCoord = y;
                    newGridSquare.ImageURL = "sea-hex.png";
                    newGridSquare.Type = "sea";
                    dbContext.GridSquares.Add(newGridSquare);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("AdminDashboard", "Admin");
        }
        [HttpPost("createport")]
        public IActionResult NewPort(Port newPort)
        {
            if(ModelState.IsValid)
            {
                dbContext.Ports.Add(newPort);
                dbContext.SaveChanges();
                return RedirectToAction("AdminDashboard","Admin");
            }
            return View("AdminDashboard");
        }
        [HttpGet("wholemap")]
        public string WholeMap()
        {
            int xCount = dbContext.GridSquares.Max(x => x.xCoord) + 1;
            int yCount = dbContext.GridSquares.Max(y => y.yCoord) + 1;
            List<List<GridSquare>> Grid = new List<List<GridSquare>>();
            if(Grid == null)
            {
                return "Please make a grid!";
            }
            for(var i = 0; i < yCount; i++)
            {
                List<GridSquare> row = dbContext.GridSquares.Where(g => g.yCoord == i).ToList();
                Grid.Add(row);
            }
            return JsonConvert.SerializeObject(Grid, Formatting.Indented);
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Landing", "Admin");
        }
    }
}