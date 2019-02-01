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
                    ModelState.AddModelError("LoginEmail", "Invalid Email");
                    return View("LoginReg");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password,userSubmission.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "That isn't the correct password for this email address!");
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

        // ****ADMINDASHBOARD****
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
            List<GridSquare> allIslands = dbContext.GridSquares.ToList();
            List<PortPrice> allPortPrices = dbContext.PortPrices.ToList();
            ViewBag.Prices = allPortPrices;
            ViewBag.Ports = allPorts;
            ViewBag.Ships = allShips;
            ViewBag.TradeGoods = allTradeGoods;
            ViewBag.Islands = allIslands;
            ViewBag.Admin = userInDb.FirstName + " " + userInDb.LastName;
            List<GridSquare> gridsToMap = dbContext.GridSquares.ToList();
            ViewBag.xMax = dbContext.GridSquares.Max(x => x.xCoord);
            ViewBag.yMax = dbContext.GridSquares.Max(y => y.yCoord);
            ViewBag.portMax = allPorts.Count;
            return View();
        }
        // ****CREATES NEW MAP****
        [HttpPost("mapsize")]
        public IActionResult MapSize (int mapSizeY, int mapSizeX)
        {
            dbContext.Database.ExecuteSqlCommand("DELETE FROM GridSquares");
            dbContext.SaveChanges();
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
        // ****CREATES PORT****
        [HttpPost("createport")]
        public IActionResult NewPort(Port newPort)
        {
            if(ModelState.IsValid)
            {
                dbContext.Ports.Add(newPort);
                dbContext.SaveChanges();
                return RedirectToAction("AdminDashboard");
            }
            return View("AdminDashboard");
        }
        [HttpGet("deletePort/{portId}")]
        public IActionResult DeletePort(int portId)
        {
            Port goodbyePort = dbContext.Ports.FirstOrDefault(p => p.PortId == portId);
            dbContext.Ports.Remove(goodbyePort);
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard","Admin");
        }
        // ****GENERATES MAP****
        [HttpGet("wholemap")]
        public string WholeMap()
        {
            int xCount = dbContext.GridSquares.Max(x => x.xCoord) + 1;
            int yCount = dbContext.GridSquares.Max(y => y.yCoord) + 1;
            List<List<GridSquare>> Grid = new List<List<GridSquare>>();
            for(var i = 0; i < yCount; i++)
            {
                List<GridSquare> row = dbContext.GridSquares.Where(g => g.yCoord == i).ToList();
                Grid.Add(row);
            }
            return JsonConvert.SerializeObject(Grid, Formatting.Indented);
        }
        // ****CREATE NEW ISLAND****
        [HttpPost("newIsland")]
        public IActionResult NewIsland(int XCoord, int YCoord)
        {
            GridSquare newIsland = dbContext.GridSquares.Where(g => g.xCoord == XCoord && g.yCoord == YCoord).FirstOrDefault();
            newIsland.Type = "land";
            newIsland.ImageURL = "land-hex.png";
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard","Admin");
        }
        [HttpGet("deleteIsland/{gridSquareId}")]
        public IActionResult DeleteIsland(int gridSquareId)
        {
            GridSquare goodbyeIsland = dbContext.GridSquares.FirstOrDefault(g => g.GridSquareId == gridSquareId);
            goodbyeIsland.Type = "sea";
            goodbyeIsland.ImageURL = "sea-hex.png";
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard","Admin");
        }
        // ****CREATE NEW PORT ISLAND****
        [HttpPost("newPortIsland")]
        public IActionResult NewPortIsland(int XCoord, int YCoord, int portId)
        {
            GridSquare newPortIsland = dbContext.GridSquares.Where(g => g.xCoord == XCoord && g.yCoord == YCoord).FirstOrDefault();
            Port addPort = dbContext.Ports.FirstOrDefault(p => p.PortId == portId);
            newPortIsland.Type = "port";
            newPortIsland.ImageURL = "port-hex.png";
            newPortIsland.Port = addPort;
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard", "Admin");
        }
        [HttpGet("deletePortIsland/{gridSquareId}")]
        public IActionResult DeletePortIsland(int gridSquareId)
        {
            dbContext.Database.ExecuteSqlCommand($"UPDATE GALLEASS.GridSquares SET PortId = NULL WHERE (GridSquareId = {gridSquareId});");
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard","Admin");

        }
        // ****CREATE NEW TRADEGOOD****
        [HttpPost("createTradeGood")]
        public IActionResult CreateTradeGood(TradeGood newTradeGood)
        {
            if(ModelState.IsValid)
            {
                dbContext.TradeGoods.Add(newTradeGood);
                dbContext.SaveChanges();
                return RedirectToAction ("AdminDashboard", "Admin");
            }
            return View("AdminDashboard");
        }
        [HttpGet("deleteTradeGood/{tradeGoodId}")]
        public IActionResult DeleteTradeGood(int tradeGoodId)
        {
            TradeGood goodbyeTradeGood = dbContext.TradeGoods.FirstOrDefault(t => t.TradeGoodId == tradeGoodId);
            dbContext.TradeGoods.Remove(goodbyeTradeGood);
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard", "Admin");
        }
        [HttpGet("deletePortPrice/{portPriceId}")]
        public IActionResult DeletePortPrice(int portPriceId)
        {
            PortPrice goodbyePortPrice = dbContext.PortPrices.FirstOrDefault(pp => pp.PortPriceId == portPriceId);
            dbContext.Remove(goodbyePortPrice);
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard","Admin");
        }
        // ****CREATES PORTPRICES****
        [HttpPost("createPortPrice")]
        public IActionResult CreatePortPrice(PortPrice newPortPrice)
        {
            if(ModelState.IsValid)
            {
                dbContext.PortPrices.Add(newPortPrice);
                dbContext.SaveChanges();
                return RedirectToAction("AdminDashboard","Admin");
            }
            return View("AdminDashboard");
        }
        // ****CREATES VESSELTYPE****
        [HttpPost("createship")]
        public IActionResult CreateShip(VesselType newVessel)
        {
            if(ModelState.IsValid)
            {
                dbContext.VesselTypes.Add(newVessel);
                dbContext.SaveChanges();
                return RedirectToAction("AdminDashboard","Admin");
            }
            return View("AdminDashboard");
        }
        [HttpGet("deleteVesselType/{VesselTypeId}")]
        public IActionResult DeleteShip(int vesselTypeId)
        {
            VesselType goodbyeVessel = dbContext.VesselTypes.FirstOrDefault(v => v.VesselTypeId == vesselTypeId);
            dbContext.VesselTypes.Remove(goodbyeVessel);
            dbContext.SaveChanges();
            return RedirectToAction("AdminDashboard", "Admin");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Landing", "Admin");
        }
    }
}