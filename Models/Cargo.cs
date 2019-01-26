using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galleass.Models
{
    public class Cargo
    {
        [Key]
        public int CargoId {get;set;}
        public int Quantity {get;set;}
        //Many to Many(A TradeGood can be in many players cargo.)
        public int TradeGoodId {get;set;}
        public TradeGood TradeGood {get;set;}
        //Many to Many(A Player can have many TradeGoods in their cargo.)
        public int PlayerId {get;set;}
        public Player Player {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        
    }
}