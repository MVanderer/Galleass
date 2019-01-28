using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Galleass.Models
{
    public class Supply
    {
        [Key]
        public int SupplyId {get;set;}
        public string name {get;set;}
        public float baseCost {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        

    }
}