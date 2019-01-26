using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Galleass.Models
{
    public class Discovered
    {
        [Key]
        public int DiscoverdId {get;set;}
        //Many to many(Player to GridSquare)
        public int PlayerId {get;set;}
        public Player Player {get;set;}
        //Many to many(GridSquare to Player)
        public int GridSquareId {get;set;}
        public GridSquare GridSquare {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}