using TheWall.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;


namespace TheWall.Models
{
    public class ModelBundle
    {
        public User UserModel { get; set; }
        public List<Message> MessageModel { get; set; }
        public List<Comment> CommentModel {get;set;}
    }
}