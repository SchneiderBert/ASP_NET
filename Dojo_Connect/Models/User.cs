using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dojo_Connect.Models {
    public class User {
        [Key]
        public int UserId {get;set;}

        [Required]
        [Display(Name= "Name")]

        public string Name {get;set;}
        [Required]
        [Display(Name= "Alias")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Alias must consist of only letters and numbers")]
        public string Alias {get;set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password Must Be At Least 8 Characters")]
        public string Password {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;


        [NotMapped]

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]

        public string Confirm {get;set;}




    }
}