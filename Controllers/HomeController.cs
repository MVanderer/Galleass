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
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg", "Admin");
            }
            int? signedInId = HttpContext.Session.GetInt32("UserId");
            User SignedIn = dbContext.Users.FirstOrDefault(u => u.UserId == signedInId);
            List<Player> UserPlayers = dbContext.Players.Where(p => p.UserId == SignedIn.UserId).ToList();
            ViewBag.Players = UserPlayers;
            ViewBag.User = SignedIn.FirstName;
            return View();
        }
        [HttpGet("newgame/{slot}")]
        public IActionResult NewGame(int slot)
        {
            List<VesselType> vessels = dbContext.VesselTypes.ToList();
            ViewBag.Vessels = vessels;
            ViewBag.Player = slot;
            return View();
        }
        [HttpPost("createplayer")]
        public IActionResult CreatePlayer(Player newPlayer, int Slot)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            newPlayer.Slot = Slot;
            VesselType Vessel = dbContext.VesselTypes.FirstOrDefault(v => v.VesselTypeId == newPlayer.VesselTypeId);
            if(newPlayer.Crew < Vessel.MinCrew || newPlayer.Crew > Vessel.MaxCrew)
            {
                newPlayer.Crew = Vessel.MinCrew;
            }
            int PortMin = dbContext.Ports.Min(p => p.PortId);
            GridSquare PlayerStart = dbContext.GridSquares.FirstOrDefault(g => g.Port.PortId == PortMin);
            newPlayer.GridSquareId = PlayerStart.GridSquareId;
            newPlayer.UserId = (int)id;
            newPlayer.Wealth = 1000;
            dbContext.Players.Add(newPlayer);
            dbContext.SaveChanges();
            HttpContext.Session.SetInt32("Wealth", newPlayer.Wealth);
            HttpContext.Session.SetInt32("PlayerId",Slot);
            HttpContext.Session.SetInt32("X",PlayerStart.xCoord);
            HttpContext.Session.SetInt32("Y", PlayerStart.yCoord);
            return RedirectToAction("World", "Home");
        }
        [HttpGet("continuegame/{Slot}")]
        public IActionResult ContinueGame(int Slot)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            System.Console.WriteLine($"this is user id {(int)id}");
            Player Playing = dbContext.Players.FirstOrDefault(p => p.Slot == Slot);
            GridSquare location = dbContext.GridSquares.FirstOrDefault(g => g.GridSquareId == Playing.GridSquareId);
            HttpContext.Session.SetInt32("Wealth", Playing.Wealth);
            HttpContext.Session.SetInt32("PlayerId", Playing.Slot);
            HttpContext.Session.SetInt32("X",location.xCoord);
            HttpContext.Session.SetInt32("Y", location.yCoord);
            return RedirectToAction("World","Home");
        }

        [HttpGet("Ship")]
        public IActionResult ShipDetails(){
            int? slot = HttpContext.Session.GetInt32("PlayerId");
            Player Playing = dbContext.Players.FirstOrDefault(p => p.Slot == slot);
            VesselType vessel = dbContext.VesselTypes.FirstOrDefault(v => v.VesselTypeId == Playing.VesselTypeId);
            ViewBag.Wealth = Playing.Wealth;
            ViewBag.Sailors= Playing.Crew;
            ViewBag.MinCrew= vessel.MinCrew;
            ViewBag.Food=30;
            return View();
        }

        [HttpGet("World")]
        public IActionResult World(){
            if(HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("PlayerId") == null)
            {
                return RedirectToAction("LoginReg", "Admin");
            }
            int? slot = HttpContext.Session.GetInt32("PlayerId");
            int? id = HttpContext.Session.GetInt32("UserId");
            Player Playing = dbContext.Players.FirstOrDefault(p => p.Slot == (int)slot && p.UserId == (int)id);
            return View(Playing);
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
                    GridSquare cell = dbContext.GridSquares.Include(g => g.Port).FirstOrDefault( g => g.xCoord == xIdx && g.yCoord == yIdx);
                    column.Add(cell);
                }
                Map.Add(column);
            }
            return JsonConvert.SerializeObject(Map, Formatting.Indented);
        }

        [HttpGet("getPlayerInfo")]
        public string GetPlayerInfo(){
            int? X = HttpContext.Session.GetInt32("X");
            int? Y = HttpContext.Session.GetInt32("Y");
            dynamic package = new {
            x = (int)X,
            y = (int)Y,
            name = "adrien",
                
            };

            return JsonConvert.SerializeObject(package, Formatting.Indented);
        }
        [HttpGet("updatePlayerPosition/{x}/{y}")]
        public string UpdatePlayerInfo(int x,int y )
        {
            System.Console.WriteLine("*%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%*");
            System.Console.WriteLine("New Coordinates:"+x+" & "+y);

            return "success";
        }
    }
}
