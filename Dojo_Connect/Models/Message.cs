using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Dojo_Connect.Models
{

    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public User Creator { get; set; }

        [Required]
        public int UserId {get;set;}

        [Required]
        [MinLength(1, ErrorMessage = "Message has got to be at least 1 character")]
        [Display(Name = "Message")]
        public string MessageContent {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}