using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BC.Context;
using BC.Models;

namespace BC.Controllers
{
    public class WeatherApiController : Controller
    {
        private readonly ContextDB _context;
        public WeatherApiController(ContextDB meteo)
        {
            _context = meteo;
        }
        public IActionResult Index()
        {
            var tmp = (ClaimsIdentity)HttpContext.User.Identity;
            string currentUserID = tmp.Claims.FirstOrDefault(u => u.Type == "usrID").Value;

            List<Device> device = _context.Device.Where(u => u.User.UserID == int.Parse(currentUserID)).ToList();
            return View(device.AsEnumerable()) ;
        }
    }
}
