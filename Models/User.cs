using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Galleass.Models {
    public class User {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength (3)]
        [Display (Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [MinLength (3)]
        [Display (Name = "Last Name:")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display (Name = "Email:")]
        public string Email { get; set; }
        public bool Admin{get;set;} = false;

        [Required]
        [DataType (DataType.Password)]
        [MinLength (8, ErrorMessage = "Password must be at least 8 characters long!")]
        [ValidPassword(ErrorMessage = "Password must contain at least 1 number, 1 letter, and 1 special character")]
        [Display (Name = "Password:")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [DataType (DataType.Password)]
        [Display (Name = "Confirm Password:")]
        [Compare ("Password")]
        public string ConfirmPassword { get; set; }
        //One to Many(A User can have many players)
        public List<Player> Players {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
    public class ValidPasswordAttribute : ValidationAttribute {
        public override bool IsValid (object value) {
            string password = (string)value;
            bool hasDecimalDigit = false;
            bool hasLowerCaseLetter = false;
            bool hasUpperCaseLetter = false;
            bool hasSpecialChar = false;
            foreach (char c in password) 
            {
                if (char.IsUpper (c)) hasUpperCaseLetter = true;
                else if (char.IsLower (c)) hasLowerCaseLetter = true;
                else if (char.IsDigit (c)) hasDecimalDigit = true;
                else if (!char.IsUpper (c) && !char.IsDigit(c) && !char.IsDigit(c)) hasSpecialChar = true;
            }
            if(hasDecimalDigit == true && hasLowerCaseLetter == true && hasUpperCaseLetter == true && hasSpecialChar == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}