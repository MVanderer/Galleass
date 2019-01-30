using System;
using System.Collections.Generic;
using System.Linq;
using Galleass.Data;
using Galleass.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Galleass.Controllers
{
    public class HomeController : Controller
    {
        private DataContext dbContext;
        public HomeController (DataContext context) {
            dbContext = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("GameMenu")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Ship")]
        public IActionResult ShipDetails(){
            ViewBag.Sailors=34;
            ViewBag.MinCrew=24;
            ViewBag.Food=30;
            return View();
        }

        [HttpGet("World")]
        public IActionResult World(){
            return View();
        }

        [HttpGet("Port")]
        public IActionResult Port(){
            ViewBag.PortName = "Arthur";
            
            List<string> SoldGoods=new List<string>();
            SoldGoods.Add("Food");
            SoldGoods.Add("Fresh Water");
            SoldGoods.Add("Textiles");
            SoldGoods.Add("Spices");
            ViewBag.SoldGoods=SoldGoods;
            List<string> PlayerCargo=new List<string>();
            PlayerCargo.Add("Food");
            PlayerCargo.Add("Fresh Water");
            PlayerCargo.Add("Textiles");
            PlayerCargo.Add("Spices");
            ViewBag.PlayerCargo=PlayerCargo;
            
            return View("PortMain");
        }

    }
}
