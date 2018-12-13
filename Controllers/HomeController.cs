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
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        //DISPLAY LOGIN REG PAGE
        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("loggedin") == "true")
            {
                return RedirectToAction("Wall");
            }
            else
            {
                return View("Index");
            }
        }

        //VIEW THE WALL!!!
        [HttpGet("/wall")]
        public IActionResult Wall()
        {
            if (HttpContext.Session.GetString("loggedin") == "true")
            {
                // ModelBundle ViewModel = new ModelBundle{
                //     UserModel=dbContext.users.FirstOrDefault(u =>u.UserId == HttpContext.Session.GetInt32("id")),
                //     MessageModel=dbContext.messages.ToList(),
                //     CommentModel=dbContext.comments.ToList()};

                ViewBag.User = dbContext.users.FirstOrDefault(u =>u.UserId == HttpContext.Session.GetInt32("id"));
                ViewBag.MLikes = dbContext.mlikes.ToList();
                ViewBag.CLikes = dbContext.clikes.ToList();
                ViewBag.MessagesWithAuthors = dbContext.messages
                .OrderBy(m => m.CreatedAt)
                .Include(m => m.Author)
                // .Include(m => m.Comments)
                // .OrderByDescending(c => c.CreatedAt)
                // .ThenInclude(c => c.Author)
                .ToList();
                ViewBag.CommentsWithAuthors = dbContext.comments
                .OrderByDescending(c => c.CreatedAt)
                .Include(c => c.Author)
                .ToList();
                ViewBag.wall = " active";ViewBag.profile = "";
                ViewBag.id = HttpContext.Session.GetInt32("id");
                return View("wall");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
