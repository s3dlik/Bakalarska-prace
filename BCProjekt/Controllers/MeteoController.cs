using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BC.Context;
using BC.Helpers;
using BC.Models;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BC.Controllers
{
    [Authorize]
    public class MeteoController : Controller
    {
        private readonly ContextDB _context;
        
        public MeteoController(ContextDB meteo)
        {
            _context = meteo;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Create(int? id)
        {
            if (id == null) { 
                var identity = (ClaimsIdentity)HttpContext.User.Identity;
                string currentUserID = identity.Claims.FirstOrDefault(u => u.Type == "usrID").Value;
                List<Device> devices = _context.Device.Where(u => u.User.UserID == int.Parse(currentUserID)).ToList();
                ViewData["Stations"] = devices;
                return View();
            }
            else {
                var device = _context.Device.Where(e => e.DevID == id).ToList();
                Device dev = new();
                foreach (var item in device)
                {
                    dev.Address = item.Address;
                    dev.Protocol = item.Protocol;
                    dev.PrefferedCommunication = item.PrefferedCommunication;
                    dev.LoadType = item.LoadType;
                    dev.Geolocation = item.Geolocation;
                    dev.TimeDelay = item.TimeDelay;
                    dev.Units = item.Units;
                    dev.Manufacturer = item.Manufacturer;
                }
                
                return View(dev);
            }
        }
        [HttpPost]
        public IActionResult Create(Device device, string options, string loadType, string protocol, IFormCollection values)
        {
            bool exists = true;

            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string currentUserID = identity.Claims.FirstOrDefault(u => u.Type == "usrID").Value;
            User user = _context.User.FirstOrDefault(u => (u.UserID == int.Parse(currentUserID)));

            var existingDevices = _context.Device.Where(e=>e.User.UserID == user.UserID);
            var onlyDevicesNames = existingDevices.Select(u => u.Name).ToList();

            if (!onlyDevicesNames.Contains(device.Name))
                exists = false;
            
            if(!exists){
                if (!ModelState.IsValid)
                {
                    return View(device);
                    
                }
                singleData.returnInstance().Communicaton = values["options"];
                singleData.returnInstance().DataType = values["LoadType"];
                singleData.returnInstance().Protocol = values["protocol"];
                var units = values["Units"];
                var senTmp = values["sensorsSelect"];
                List<Sensors> sensorItem = new();
                
                for (int i = 0; i < senTmp.Count; i++)
                {
                    Sensors sensor = new();
                    sensor.Name = senTmp[i];
                    sensorItem.Add(sensor);
                }
                

                if (!string.IsNullOrEmpty(values["URLFormat"])) { 
                    singleData.returnInstance().URLFormat = values["URLFormat"];
                    device.UrlFormat = values["URLFormat"];
                }

                device.User = user;
                device.LoadType = loadType;
                device.Protocol = protocol;
                device.Sensors = sensorItem;
                device.Units = units;
                device.PrefferedCommunication = options;
                _context.Device.Add(device);
                
                _context.SaveChanges();
                
                
                return RedirectToAction("Index", "Home");
            }
            ViewData["CreateModelError"] = "Device with that name already exists!";
            return View();
        }
        public IActionResult ListStations()
        {
            var tmp = (ClaimsIdentity)HttpContext.User.Identity;
            string currentUserID = tmp.Claims.FirstOrDefault(u => u.Type == "usrID").Value;
            List<Device> devices = _context.Device.Where(u=>u.User.UserID == int.Parse(currentUserID)).ToList();
            
            return View(devices.AsEnumerable());
        }
        public async Task <IActionResult> Edit(int? id)
        {
            var device = await _context.Device.FindAsync(id);

            var getSensors = _context.Device.Include(tmp => tmp.Sensors).FirstOrDefault(u => u.DevID == id);
            var selectedSensors = getSensors.Sensors;
            device.Sensors = selectedSensors;
            return View(device);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Device device, int id, IFormCollection values)
        {
            if (id != device.DevID)
                return NotFound();
            //Device dev = _context.Device.FirstOrDefault(u=> u.DevID == values["DevID"]);
            if (ModelState.IsValid)
            {
                var sensors = values["ItemName"];
                var senTmp = values["editSensorsSel"];
                List<Sensors> sensorItem = new();

                for (int i = 0; i < sensors.Count; i++)
                {
                    Sensors sensor = new();
                    sensor.Name = sensors[i];
                    sensorItem.Add(sensor);
                }
                for (int i = 0; i < senTmp.Count; i++)
                {
                    Sensors sensor = new();
                    sensor.Name = senTmp[i];
                    sensorItem.Add(sensor);
                }
                var oldItems = _context.Sensors.Where(sen => sen.Device.DevID == device.DevID);
                _context.Sensors.RemoveRange(oldItems);
                device.Sensors = sensorItem;
                device.PrefferedCommunication = values["options"];
                _context.Update(device);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListStations");
            }
            return View(device);
        }
        public IActionResult Details(int? id)
        {
            Device device = _context.Device.Find(id);
            //
            return View(device);
        }


        public IActionResult Delete(int? id)
        {
            Device devices = _context.Device.Find(id);
            return View(devices);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var devices = await _context.Device.FindAsync(id);
            var dev = _context.Device.Where(e => e.DevID == id).Include(e => e.Sensors).First();

            _context.Remove(dev);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListStations");
        }


        public IActionResult AdvancedDetails(int ? id)
        {
            Device device = _context.Device.Find(id);
            var getSensors = _context.Device.Include(tmp => tmp.Sensors).FirstOrDefault(u=>u.DevID==id);
            var selectedSensors = getSensors.Sensors;
            device.Sensors = selectedSensors;

            return View(device);
        }
    }
}
