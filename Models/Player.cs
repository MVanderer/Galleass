using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Galleass.Models
{
    public class Player
    {
        [Key]
        public int PlayerId {get;set;}

        public int Wealth {get;set;}
        public string PlayerName {get;set;}
        public string ShipName {get;set;}
        public int Slot {get;set;}
        public int HullCondition {get;set;}
        public int SailsCondition {get;set;}
        public int RiggingCondition {get;set;}
        public int CrewCondition {get;set;}
        public int Crew {get;set;}
        //One to many(Player can have many vesseltypes)
        public int VesselTypeId {get;set;}
        public VesselType VesselType {get;set;}
        //Many to many(Player can have discovered many GridSquares)
        public List<Discovered> Discoveries {get;set;}
        //One to Many(Player can have one GridSquare)
        public int GridSquareId {get;set;}
        //Many to Many(A player can have many TradeGoods)
        public List<Cargo> Cargo {get;set;}
        //One to Many(A player can have one User.)
        public int UserId {get;set;}
        public User User {get;set;}
        public GridSquare GridSquare {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdateAt {get;set;} = DateTime.Now;

    }
}
