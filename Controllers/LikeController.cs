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
    public class LikeController : Controller
    {
        private MyContext dbContext;
        public LikeController(MyContext context)
        {
            dbContext = context;
        }

        //ADD MESSAGE LIKE
        [HttpPost("/addmlike")]
        public IActionResult AddMLike(MLike newmlike)
        {
            if (ModelState.IsValid)
            {
                dbContext.mlikes.Add(newmlike);
                dbContext.SaveChanges();
                return RedirectToAction("Wall", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //MESSAGE UNLIKE
        [HttpPost("/unlikemessage")]
        public IActionResult UnlikeMessage(int MLikeId)
        {
            if (HttpContext.Session.GetString("loggedin") == "true")
            {
                MLike mymlike = dbContext.mlikes.FirstOrDefault(c => c.MLikeId == MLikeId);
                if (mymlike.UserId == HttpContext.Session.GetInt32("id"))
                {
                    dbContext.mlikes.Remove(mymlike);
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


        //ADD COMMENT LIKE
        [HttpPost("/addclike")]
        public IActionResult AddCLike(CLike newclike)
        {
            if (ModelState.IsValid)
            {
                dbContext.clikes.Add(newclike);
                dbContext.SaveChanges();
                return RedirectToAction("Wall", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //COMMENT UNLIKE
        [HttpPost("/unlikecomment")]
        public IActionResult UnlikeComment(int CLikeId)
        {
            if (HttpContext.Session.GetString("loggedin") == "true")
            {
                CLike myclike = dbContext.clikes.FirstOrDefault(c => c.CLikeId == CLikeId);
                if (myclike.UserId == HttpContext.Session.GetInt32("id"))
                {
                    dbContext.clikes.Remove(myclike);
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
