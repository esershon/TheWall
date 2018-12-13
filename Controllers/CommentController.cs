using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TheWall.Models;
using Microsoft.EntityFrameworkCore; //need this one for LINQ joins to work
using System.Text;

namespace TheWall.Controllers
{
    public class CommentController : Controller
    {
        private MyContext dbContext;
        public CommentController(MyContext context)
        {
            dbContext = context;
        }
                //ADD COMMENT
        [HttpPost("/addcomment")]
        public IActionResult AddComment(Comment newcomment)
        {
            if (ModelState.IsValid)
            {
                dbContext.comments.Add(newcomment);
                dbContext.SaveChanges();
                return RedirectToAction("Wall", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //DELETE COMMENT
        [HttpGet("/deletecomment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            if (HttpContext.Session.GetString("loggedin") == "true")
            {
                Comment mycomment = dbContext.comments.FirstOrDefault(c => c.CommentId == id);
                if (mycomment.UserId == HttpContext.Session.GetInt32("id"))
                {
                    dbContext.comments.Remove(mycomment);
                    dbContext.SaveChanges();
                    return RedirectToAction("Wall", "Home");
                }
                else
                {
                    return RedirectToAction("Wall", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
