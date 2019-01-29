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
            return View("Port/PortMain");
        }

        [HttpGet("Wharf")]
        public IActionResult PortWarf(){
            
            return View("Port/PortWharf");
        }

        [HttpGet("Tavern")]
        public IActionResult PortTavern(){
            return View("Port/PortTavern");
        }

        [HttpGet("Market")]
        public IActionResult PortMarket(){
            return View("Port/PortMarket");
        }


    }
}
