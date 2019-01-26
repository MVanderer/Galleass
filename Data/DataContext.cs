using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Galleass.Models;

namespace Galleass.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions options) : base (options) { }
        DbSet<User> Users {get;set;}
        DbSet<Player> Players {get;set;}
        DbSet<Cargo> Cargos {get;set;}
        DbSet<Discovered> Discovereds {get;set;}
        DbSet<GridSquare> GridSquares {get;set;}
        DbSet<Port> Ports {get;set;}
        DbSet<PortPrice> PortPrices {get;set;}
        DbSet<VesselType> VesselTypes {get;set;}
        DbSet<TradeGood> TradeGoods {get;set;}
        

    }
}