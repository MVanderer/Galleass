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
    public class AdminController : Controller
    {
        private DataContext dbContext;
        public AdminController (DataContext context) 
        {
            dbContext = context;
        }
        [HttpGet]
        [Route("/home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}