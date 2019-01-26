using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galleass.Models
{
    public class GridSquare
    {
        [Key]
        public int GridSquareId {get;set;}
        public int xCoord {get;set;}
        public int yCoord {get;set;}
        public string Type {get;set;}
        public string ImageURL {get;set;}
        // Many to many (GridSquare has many players that discovered it.)
        public List<Discovered> Discoveries {get;set;}
        //One to many(GridSquare has many players on it.)
        public List<Player> Players {get;set;}
        //One to one(A GridSquare doesn't need a Port.)
        public int PortId {get;set;}
        public Port Port {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}