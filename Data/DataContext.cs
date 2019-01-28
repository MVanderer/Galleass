using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Galleass.Models;

namespace Galleass.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions options) : base (options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Player> Players {get;set;}
        public DbSet<Cargo> Cargos {get;set;}
        public DbSet<Discovered> Discovereds {get;set;}
        public DbSet<GridSquare> GridSquares {get;set;}
        public DbSet<Port> Ports {get;set;}
        public DbSet<PortPrice> PortPrices {get;set;}
        public DbSet<VesselType> VesselTypes {get;set;}
        public DbSet<TradeGood> TradeGoods {get;set;}
        

    }
}