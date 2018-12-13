using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TheWall.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.-]+$", ErrorMessage = "First name must only contain letters.")]
        [MinLength(2, ErrorMessage="First name must be 2 characters or longer")]
        [Display(Name="First Name")]
        public string FName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.-]+$", ErrorMessage = "First name must only contain letters.")]
        [MinLength(2, ErrorMessage="Lirst name must be 2 characters or longer")]
        [Display(Name="Last Name")]
        public string LName { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer")]

        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Compare("Password", ErrorMessage = "Password confirmation must match password")]
        [DataType(DataType.Password)]
        [Display(Name="Password Confirm")]
        public string PwConfirm { get; set; }
        public List<Message> MessagesAuthored {get;set;}//one to many

        public List<Comment> CommentsAuthored {get;set;}//one to many
        public List<MLike> MessagesLiked {get;set;}//many to many
        public List<CLike> CommentsLiked {get;set;}//many to many
        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;
    }   
}
