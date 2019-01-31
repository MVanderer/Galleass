using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Galleass.Models
{
    public class VesselType
    {
        [Key]
        public int VesselTypeId {get;set;}
        public string TypeName {get;set;}
        public int CargoSpace {get;set;}
        public int TopSpeed {get;set;}
        public string VesselImageURL {get;set;}
        public bool Oars {get;set;}
        public int MinCrew {get;set;}
        public int MaxCrew {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}