using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TheWall.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int UserId {get;set;}//one to many
        public User Author {get;set;}//one to many
        [Required]
        public string Content { get; set; }
        public List<CLike> Likes {get;set;}//many to many
        public Message ParentMessage {get;set;}//one to many
        public int MessageId {get;set;}//one to many

        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;
    }   
}