using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
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
        [PasswordCheck(ErrorMessage="Your password must have at least one letter, one number, and one special character")]
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
    public class PasswordCheckAttribute : ValidationAttribute {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            string valStr = Convert.ToString (value);
            string regex = @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])";
            var match = Regex.Match(valStr,regex,RegexOptions.IgnoreCase);
            if (match.Success){
                return ValidationResult.Success;
            }
            else {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}