using System;
using System.Collections.Generic;
using System.Linq;
using Galleass.Data;
using Galleass.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        [HttpGet("RenderMap/{OriginX}/{OriginY}/{RangeX}/{RangeY}")]
        public String RenderMap(int OriginX, int OriginY, int RangeX, int RangeY)
        {
            System.Console.WriteLine("Getting map segment");
            System.Console.WriteLine("*************************************************************");
            System.Console.WriteLine(OriginX+" "+OriginY);
            List<List<GridSquare>> Map = new List<List<GridSquare>>();
            for(int yIdx = OriginY - RangeY; yIdx <= OriginY + RangeY; yIdx ++)
            {
                List<GridSquare> column = new List<GridSquare>();
                for(int xIdx = OriginX - RangeX; xIdx <= OriginX + RangeX; xIdx ++)
                {
                    GridSquare cell = dbContext.GridSquares.FirstOrDefault( g => g.xCoord == xIdx && g.yCoord == yIdx);
                    column.Add(cell);
                }
                Map.Add(column);
            }
            return JsonConvert.SerializeObject(Map, Formatting.Indented);
        }

    }
}
