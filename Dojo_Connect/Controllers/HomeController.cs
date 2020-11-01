using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dojo_Connect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Dojo_Connect.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                User UserinDB = _context.Users.SingleOrDefault(u => u.Email == newUser.Email);
                if (UserinDB != null)
                {

                    ModelState.AddModelError("Email", "Email Already In Use");
                    return View("Index");
                }

                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.Password = hasher.HashPassword(newUser, newUser.Password);
                Console.WriteLine(newUser.Password);
                _context.Users.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("loggedUser", newUser.UserId);
                return RedirectToAction("DojoChat");
            }

            return View("Index");
        }
        [HttpPost("Login")]
        public IActionResult Login(LUser loguser)
        {
            if (ModelState.IsValid)
            {
                User UserinDB = _context.Users.SingleOrDefault(u => u.Email == loguser.LEmail);
                if (UserinDB == null)
                {

                    ModelState.AddModelError("LEmail", "Failed Login Attempt");
                    return View("Index");
                }

                PasswordHasher<LUser> hasher = new PasswordHasher<LUser>();

                PasswordVerificationResult result = hasher.VerifyHashedPassword(loguser, UserinDB.Password, loguser.LPassword);

                if (result == 0)
                {
                    ModelState.AddModelError("LEmail", "Failed Login Attempt");
                    return View("Index");
                }
                Console.WriteLine($"User in DB ID: {UserinDB.UserId}");
                int id = (int)UserinDB.UserId;

                HttpContext.Session.SetInt32("loggedUser", UserinDB.UserId);
                Console.WriteLine(HttpContext.Session.GetInt32("loggedUser"));

                return RedirectToAction("DojoChat");
            }

            return View("Index");
        }

        [HttpGet("dojo_chat")]

        public IActionResult DojoChat(){
              ViewBag.LoggedUser = _context.Users.SingleOrDefault(U => U.UserId == HttpContext.Session.GetInt32("loggedUser"));
            if (ViewBag.LoggedUser == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ChatLog = _context.ChatLog.Include( m => m.Creator).OrderBy( m => m.CreatedAt).ToList();

            return View();
        }


    }
}
