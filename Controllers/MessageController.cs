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
    public class MessageController : Controller
    {
        private MyContext dbContext;
        public MessageController(MyContext context)
        {
            dbContext = context;
        }

        //ADD MESSAGE
        [HttpPost("/addmessage")]
        public IActionResult AddMessage(Message newmessage)
        {
            // if (ModelState.IsValid)
            // {
            dbContext.messages.Add(newmessage);
            dbContext.SaveChanges();
            return RedirectToAction("Wall", "Home");
            // }
            // else
            // {
            //     return RedirectToAction("Index"); //I decided not to return a View Response since the only validation I have is [Required] and I don't think they need a reminder about that.
            // }
        }

        //DELETE MESSAGE
        [HttpGet("/deletemessage/{id}")]
        public IActionResult DeleteMessage(int id)
        {
            if (HttpContext.Session.GetString("loggedin") == "true")
            {
                Message mymessage = dbContext.messages.FirstOrDefault(m => m.MessageId == id);
                if (mymessage.UserId == HttpContext.Session.GetInt32("id"))
                {
                    dbContext.messages.Remove(mymessage);
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
                return RedirectToAction("Index");
            }
        }
    }
}

