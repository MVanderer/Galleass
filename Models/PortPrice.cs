using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galleass.Models
{
    public class PortPrice
    {
        [Key]
        public int PortPriceId {get;set;}
        public float BuyModifier {get;set;}
        public float SellModifier {get;set;}
        public int QuantityAvailable {get;set;}
        //Many to many (Ports and TradeGoods)
        public int PortId {get;set;}
        public Port Port {get;set;}
        //Many to many (TradeGoods and Ports)
        public int TradeGoodId {get;set;}
        public TradeGood TradeGood {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}