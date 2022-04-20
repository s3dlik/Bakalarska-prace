using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Dynamic;
using Microsoft.AspNetCore.Authentication;
using BC.Context;
using System.Timers;
using BC.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Data;

namespace BC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static System.Timers.Timer aTimer;
        private static System.Timers.Timer cacheTimer;
        private readonly ILogger<HomeController> _logger;
        private readonly ContextDB _context;
        public HomeController(ILogger<HomeController> logger, ContextDB meteo)
        {
            _context = meteo;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            //Calling user identity from claims, so I get logged user ID
            var userIdentity = (ClaimsIdentity)HttpContext.User.Identity;
            string currentUserID = userIdentity.Claims.FirstOrDefault(u => u.Type == "usrID").Value;
            List<Device> stations = _context.Device.Where(u => u.User.UserID == int.Parse(currentUserID)).ToList();
            //After starting and loading application cache timer is called to check if the cache list is >= 0 and if yes, so start saving data to database
            await Task.Run(() => SaveCacheDataDB());
            //using dynamic model to send multiple object types to View
            dynamic mymodel = new ExpandoObject();
            mymodel.Data = singleData.returnInstance().dataList.AsEnumerable();
            mymodel.Device = stations.AsEnumerable();
            
            return View(mymodel);
        }

        private async Task SaveCacheDataDB()
        {
            //Timer for 30 seconds to call method that looks on count of CacheData list
            cacheTimer = new System.Timers.Timer(30000);
            cacheTimer.Elapsed += async (sender, e) => await OnTimedEventCachedata(sender, e);
            cacheTimer.Enabled = true;
        }

        private async Task OnTimedEventCachedata(object source, ElapsedEventArgs e )
        {
            
            try
            {
                //Check if count is more than 0
                if (Cacher.returnInstance().CheckCount())
                {
                    for (int i = 0; i < Cacher.returnInstance().selectedDevicesList.Count; i++)
                    {
                        //Needed new instance of ContextDB (DBContext) because ASP.NET is hard opening and closing DBContext becuase of dependency injection
                        var contextOptions = new DbContextOptionsBuilder<ContextDB>().UseSqlite(@"Data source = database.db").Options;
                        using var cont = new ContextDB(contextOptions);
                        var values = Cacher.returnInstance().CacheData.FirstOrDefault();

                        Values val = new Values();
                        val.Timestamp = DateTime.Now.ToString("dd MM yyyy HH:mm");
                        val.Value = values.Item3;
                        val.SensorID = values.Item2;
                        cont.Add(val);
                        await cont.SaveChangesAsync();
                        await cont.DisposeAsync();
                        Cacher.returnInstance().CacheData.RemoveAll(x => x.Item1 == values.Item1);
                    }
                    
                }

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                RedirectToAction("ErrorPage");
            }
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Login method using Claims - Built in extension using ASPNetCore Authentizaion
        public async Task<ActionResult> Login(IFormCollection user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var searchUser = _context.User.Where(u=> (u.Login.Equals(user["Login"])) || (u.Email.Equals(user["Email"]))).FirstOrDefault();
                //Verifying user and user hashed password. Savety first!
                if ((searchUser != null) && Crypto.VerifyHashedPassword(searchUser.Password, user["Password"])){
                    var claims = new List<Claim>
                    {
                        new Claim("user", searchUser.Login),
                        new Claim("role", searchUser.GetType().ToString()),
                        new Claim("usrID", searchUser.UserID.ToString())
                    };
                    await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                    searchUser.LastLogin = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    ViewBag.Message = "Wrong user credentials!";
                }
            }
            
            return View();
        }
        //Logout method for user
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        //After page loads, user can choose one or multiple weather stations and after that, this method is called.
        //This method will show first loaded data from address and also it will save those first data into Cache list
        public async Task<IActionResult> GetStation(int? id)
        {
            if (!Cacher.returnInstance().selectedDevicesList.Contains((int)id)) {
                Cacher.returnInstance().selectedDevicesList.Add((int)id);
                Cacher.returnInstance().selectedDevicesCount++;
            }

            Device device = await _context.Device.FindAsync(id);
            
            var getSensors = await _context.Device.Include(tmp => tmp.Sensors).FirstOrDefaultAsync(u => u.DevID == id);
            var onlySensors = await _context.Sensors.Where(e=>e.Device != null).ToListAsync();
            
            singleData.returnInstance().Address = device.Address;
            singleData.returnInstance().TimeDelay = device.TimeDelay.ToString();
            singleData.returnInstance().Communicaton = device.PrefferedCommunication;
            singleData.returnInstance().DataType = device.LoadType;
            singleData.returnInstance().Protocol = device.Protocol;
            singleData.returnInstance().Unit = device.Units;
            singleData.returnInstance().deviceSensors = getSensors.Sensors.ToList();


            //to be sure that if user selects one of his meteostations it gets data immediately
            if (device.PrefferedCommunication.ToLower() == "get")
            {
                if (device.LoadType.ToLower() == "xml") {
                    //to get instantly fresh data if user chooses one of his meteostation, after that, method SetTimer is called
                    ReceiveGET getter = new ReceiveGET();
                    string text = getter.requestData(singleData.returnInstance().Address);
                    getter.parseDataXML(text);
                    //after first call I also need to include that data to cache list
                    foreach (var item in singleData.returnInstance().dataList)
                    {
                        foreach (var senItem in getSensors.Sensors)
                        {
                            if (item.name == senItem.Name)
                            {
                                Cacher.returnInstance().CacheData.Add(new Tuple<int,int, string>(Cacher.returnInstance().ListIndex,senItem.SenID, item.value));
                                Cacher.returnInstance().ListIndex++;
                            }
                        }
                    }
                    //after that, I call SetTimer method to run in background and it will also add items to the list
                    SetTimer(singleData.returnInstance().DataType.ToUpper());
                }
                else if(device.LoadType.ToLower() == "url")
                {
                    ReceiveGET getter = new();
                    getter.parseDataURL(device.Address, device.UrlFormat);
                }
                
            }
            else if (device.PrefferedCommunication.ToLower() == "post")
            {
                ListenerPost list = new ListenerPost();
                await Task.Run(() => list.listener());
            }


            
            //Getting only that devices that used defined he wants.
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string currentUserID = identity.Claims.FirstOrDefault(u => u.Type == "usrID").Value;
            List<Device> devices = await _context.Device.Where(u => u.User.UserID == int.Parse(currentUserID)).ToListAsync();
            dynamic mymodel = new ExpandoObject();
            mymodel.Data = singleData.returnInstance().dataList.AsEnumerable();
            mymodel.Device = devices.AsEnumerable();
            mymodel.Manufacturer = device.Manufacturer;
            return View(mymodel);
        }
        

        private void SetTimer(string format)
        {            
            
            aTimer = new System.Timers.Timer(300);
            
            
            if (format == "XML") {

                aTimer.Elapsed += (sender, e) => OnTimedEventXML(sender, e);
               
            }
            else if(format == "URL")
            {
                //Task.Run(async()=>aTimer.Elapsed += OnTimedEventURL);
                
            }
            else if(format == "CSV")
            {
                RedirectToAction("ErrorPage");
            }
            else
            {
                RedirectToAction("ErrorPage");
                //JSON
            }
            //Setting interval by user choise. Every weather station can have different time delay data downloading
            if (singleData.returnInstance().Unit.ToLower() == "minutes")
            {
                Console.WriteLine(singleData.returnInstance().TimeDelay);
                aTimer.Interval = int.Parse(singleData.returnInstance().TimeDelay) * 60000;
            }
            else
            {
                aTimer.Interval = int.Parse(singleData.returnInstance().TimeDelay) * 3600000;
            }

            aTimer.Enabled = true;
        }

        private void OnTimedEventXML(object source, ElapsedEventArgs e)
        {
            
            ReceiveGET getter = new ReceiveGET();
            Console.WriteLine(singleData.returnInstance().Address);
            string text = getter.requestData(singleData.returnInstance().Address);
            getter.parseDataXML(text);
            foreach (var item in singleData.returnInstance().dataList)
            {
                foreach (var senItem in singleData.returnInstance().deviceSensors)
                {
                    if (item.name == senItem.Name)
                    {
                        Cacher.returnInstance().CacheData.Add(new Tuple<int, int, string>(Cacher.returnInstance().ListIndex, senItem.SenID, item.value));
                        Cacher.returnInstance().ListIndex++;
                    }
                }
            }
        }

        public ContentResult getJsonMeteo()
        {
            var json = JsonSerializer.Serialize(singleMeteo.returnInstance().devicesList);
            return Content(json);
        }
        public ContentResult getJson()
        {

            var json = JsonSerializer.Serialize(singleData.returnInstance().dataList);
            return Content(json);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ErrorLogin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
