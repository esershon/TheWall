using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TheWall.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace TheWall.Controllers
{
    public class UserController : Controller
    {
        private MyContext dbContext;
        public UserController(MyContext context)
        {
            dbContext = context;
        }


        //REGISTER USER
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                //IF THE EMAIL IS ALREADY IN THE DATABASE, RETURN AN ERROR
                if (dbContext.users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("email", "Email already in use!");
                    return View("Index");
                }
                //HASH PASSWORD, ADD USER, SAVE IN SESSION
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                User myuser = dbContext.users.FirstOrDefault(u => u.Email == user.Email);
                HttpContext.Session.SetString("loggedin", "true");
                HttpContext.Session.SetInt32("id", myuser.UserId);
//SUCCESS REDIRECT
                return RedirectToAction("Wall", "Home");
            }
            return View("Index");
        }

        //LOGIN USER
        [HttpPost("/login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
                if (userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
                if (result == 0)//if the password entered was wrong
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index");
                }
                else //IF SUCCESS, SAVE IN SESSION
                {
                    HttpContext.Session.SetString("loggedin", "true");
                    HttpContext.Session.SetInt32("id", userInDb.UserId);
//SUCCESS REDIRECT
                    return RedirectToAction("Wall", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                return View("Index");
            }
        }

        //DISPLAY USER PROFILE
        [HttpGet("/user/{id}")]
        public IActionResult UserProfile(int id)
        {
            if (HttpContext.Session.GetString("loggedin") == "true") //ANYONE LOGGED IN CAN VIEW ANYONES PROFILE
            {
                User user = dbContext.users.FirstOrDefault(u => u.UserId == id);
                ViewBag.id = HttpContext.Session.GetInt32("id");
                ViewBag.user = user;
                if (HttpContext.Session.GetInt32("id")==id)
                {
                    ViewBag.wall = " active";ViewBag.profile = "";
                }
                return View("userprofile");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //DISPLAY USER EDIT PAGE
        [HttpGet("/user/edit/{id}")]
        public IActionResult EditUser(int id)
        {
            if (HttpContext.Session.GetString("loggedin") == "true")
            {
                if (HttpContext.Session.GetInt32("id")==id)
                {
                    User user = dbContext.users.FirstOrDefault(u => u.UserId == id);
                    ViewBag.user = user;
                    return View("userupdateform");
                }
                else
                {
//REDIRECT TO DASHBOARD
                    return RedirectToAction("Wall", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //UPDATE USER
        [HttpPost("user/update")]
        public IActionResult UpdateUser(User toupdate)
        {
            if (ModelState.IsValid)
            {
                User RetrievedUser = dbContext.users.FirstOrDefault(u => u.UserId==HttpContext.Session.GetInt32("id"));
                //IF THE NEW EMAIL IS IN THE DATABASE AND IT BELONGS TO SOMEONE ELSE
                if (dbContext.users.Any(u => u.Email == toupdate.Email) && toupdate.Email != RetrievedUser.Email)
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("updateuserform");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                toupdate.Password = Hasher.HashPassword(toupdate, toupdate.Password);
                RetrievedUser.FName = toupdate.FName;
                RetrievedUser.LName = toupdate.LName;
                RetrievedUser.Email = toupdate.Email;
                RetrievedUser.Password = toupdate.Password;
                RetrievedUser.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                User myuser = dbContext.users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("id"));
                ViewBag.user = myuser;
                ViewBag.id = HttpContext.Session.GetInt32("id");
                return View("userprofile");
            }
            User myself = dbContext.users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("id"));
            ViewBag.user = myself;
            return View("userupdateform");
        }

        //LOGOUT USER
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
