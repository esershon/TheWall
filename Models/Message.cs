using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TheWall.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public int UserId {get;set;}//one to many
        public User Author {get;set;}//one to many
        [Required]
        public string Content { get; set; }
        public List<MLike> Likes {get;set;}//many to many
        public List<Comment> Comments {get;set;}//one to many

        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;
    }   
}