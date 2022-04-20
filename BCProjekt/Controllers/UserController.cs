using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using BC.Context;
using BC.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.EntityFrameworkCore;

namespace BC.Controllers
{
    public class UserController : Controller
    {
        private readonly ContextDB _context;
        public UserController(ContextDB user)
        {
            _context = user;
        }
        public IActionResult Index()
        {
            var tmp = (ClaimsIdentity)HttpContext.User.Identity;
            string currentUserID = tmp.Claims.FirstOrDefault(u => u.Type == "usrID").Value;
            List<User> users = _context.User.Where(u=>u.UserID == int.Parse(currentUserID)).ToList();

            return View(users.AsEnumerable());
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _context.User.FindAsync(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user, int id, IFormCollection values)
        {
            if(id != user.UserID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    user.Password = Crypto.HashPassword(user.Password);
                    _context.Update(user);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {

                    throw e.InnerException;
                }
               
            }
            return View(user);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IFormCollection data, User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.FirstOrDefault(u => u.Login == data["Login"].ToString()) == null)
                {
                    user.Registered = DateTime.Now;
                    user.LastLogin = DateTime.Now;
                    //Hashing users password
                    user.Password = Crypto.HashPassword(user.Password);
                    _context.User.Add(user);
                

                    await _context.SaveChangesAsync();
                    TempData["Success"] = "User sucessfully added!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewData["Failure"] = "User already exists!";
                return View();
            }
            ViewData["Failure"] = "Please fix the input!";
            return View();
        }
    }
}
