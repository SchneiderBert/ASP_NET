using System.ComponentModel.DataAnnotations;

namespace Dojo_Connect.Models {
    public class LUser {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string LEmail {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string LPassword {get;set;}
        
    }
}