using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galleass.Models
{
    public class TradeGood
    {
        [Key]
        public int TradeGoodId {get;set;}
        public string GoodName {get;set;}
        public string ImageURL {get;set;}
        public float BasePrice {get;set;}
        public int Weight {get;set;}
        public int Volume {get;set;}
        //Many to many(TradeGoods can be in many different ports.)
        public List<PortPrice> PortPrice {get;set;}
        //Many to many(TradeGood can belong to many players.)
        public List<Cargo> Cargo {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}