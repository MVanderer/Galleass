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

        [HttpGet("World")]
        public IActionResult World(){
            return View();
        }
    }
}
