using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galleass.Models
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        [Display (Name="Email")]
        public string LoginEmail {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [Display (Name="Password")]
        public string LoginPassword {get;set;}
    }
}