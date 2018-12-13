using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using TheWall.Models;

namespace TheWall.Models
{
    public class MLike
    {
        [Key]
        public int MLikeId { get; set; }
        public int UserId { get; set; }
        public User Liker {get;set;}
        public int MessageId { get; set; }
        public Message Liked {get;set;}
    }
    public class CLike
    {
        [Key]
        public int CLikeId { get; set; }
        public int UserId { get; set; }
        public User Liker {get;set;}
        public int CommentId { get; set; }
        public Comment Liked {get;set;}
    }
}