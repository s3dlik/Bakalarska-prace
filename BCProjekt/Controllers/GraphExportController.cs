using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC.Context;
using BC.Helpers;
using BC.Models;

namespace BC.Controllers
{
    public class GraphExportController : Controller
    {
        private readonly ContextDB _context;
        private readonly ILogger<GraphExportController> _logger;

        public GraphExportController(ILogger<GraphExportController> logger, ContextDB meteo)
        {
            _context = meteo;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Graphs(int? id, int? idSensor, string startDate, string endDate)
        {
            //List<Sensors> test = new List<Sensors>();

            List<Sensors> test = await _context.Sensors.Where(e => e.Device.DevID == id).ToListAsync();
            List<List<Values>> values = new();
            var onlySencorsID = test.Select(e => e.SenID).ToList();

            //Adding values from database table Values with only sensorsID that is equal selected devices sensors. Add to singleton values List for further export
            if (onlySencorsID.Count > 0)
            {
                foreach (var item in onlySencorsID)
                {
                    values.Add(_context.Values.Where(e => e.SensorID == item).ToList());
                }
                //Add to maxValues list for maximum values of the day and that will be displayed on page
                foreach (var firstStep in values)
                {

                    if (!string.IsNullOrEmpty(endDate) || !string.IsNullOrEmpty(startDate))
                    {

                        if (idSensor != 0)
                        {
                            var group1 = firstStep.Where(e => e.SensorID == idSensor).Where(e => DateTime.Parse(DateTime.Parse(e.Timestamp).ToShortDateString()) >= DateTime.Parse(DateTime.Parse(startDate).ToShortDateString()) && DateTime.Parse(DateTime.Parse(e.Timestamp).ToShortDateString()) <= DateTime.Parse(DateTime.Parse(endDate).ToShortDateString())).GroupBy(e => DateTime.Parse(e.Timestamp).ToShortDateString()).OrderBy(e => e.Key);
                            foreach (var item in group1)
                            {
                                item.OrderBy(e => e.Timestamp);
                                Dictionary<string, string> dict = new Dictionary<string, string>();
                                var average = item.Select(e => decimal.Parse((e.Value), CultureInfo.InvariantCulture)).Average();
                                var min = item.Select(e => decimal.Parse((e.Value), CultureInfo.InvariantCulture)).Min();
                                var max = item.Select(e => decimal.Parse((e.Value), CultureInfo.InvariantCulture)).Max();
                                dict.Add("Date", item.Key.ToString());
                                dict.Add("Min", min.ToString());
                                dict.Add("Max", max.ToString());
                                dict.Add("Average", Math.Round(average, 2).ToString());
                                singleExportData.returnInstance().valuesToGraph.Add(dict);
                            }
                        }
                    }
                }
            }
            //selecting dates to send it to JS to datapicker so user can choose only data from dates that exists - avoiding null values
            var dates = _context.Values.Select(e => DateTime.Parse(DateTime.Parse(e.Timestamp).ToShortDateString())).ToList();
            var mindate = dates.Min().ToShortDateString();
            var maxDate = dates.Max().ToShortDateString();

            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string currentUserID = identity.Claims.FirstOrDefault(u => u.Type == "usrID").Value;
            List<Device> stations = _context.Device.Where(u => u.User.UserID == int.Parse(currentUserID)).ToList();
            //dynamic object to send multiple Models to view
            dynamic mymodel = new ExpandoObject();
            mymodel.Stations = stations;
            mymodel.Sensors = test;
            mymodel.MinDate = mindate;
            mymodel.MaxDate = maxDate;
            ViewData["DataGraph"] = getValuesToGraph();
            return View(mymodel);
        }
        public async Task<FileResult> ExportData(int? id, string? startDate, string? endDate)
        {

            //device with sensors included
            var device = _context.Device.Include(e => e.Sensors).Where(e => e.DevID == id).FirstOrDefault();
            List<Sensors> sensors = new();
            //selected only sensors from device that user wants
            sensors = await _context.Sensors.Where(e => e.Device.DevID == device.DevID).ToListAsync();


            //Dictionary of list of values + string to get all values from sensor and sensor name
            Dictionary<List<Values>, string> valuesTest = new();

            //Selecting only sensors ID
            var onlySencorsID = sensors.Select(e => e.SenID).ToList();

            //Go through sensorID's and add to dictionary values + nameof sensor
            foreach (var sensorItem in onlySencorsID)
            {

                valuesTest.Add(_context.Values.Where(e => e.SensorID == sensorItem).ToList(), _context.Sensors.Where(e => e.SenID == sensorItem).Select(e => e.Name).FirstOrDefault());

            }
            //Creating new datatable to add values into it
            DataTable dataTable = new();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Timestamp", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));

            //Go through dictionary, select sensor name and select into variable group values those are between choosen date
            foreach (var firstStep in valuesTest)
            {
                var sensorName = firstStep.Value;
                var group = firstStep.Key.Where(e => DateTime.Parse(DateTime.Parse(e.Timestamp).ToShortDateString()) >= DateTime.Parse(DateTime.Parse(startDate).ToShortDateString()) && DateTime.Parse(DateTime.Parse(e.Timestamp).ToShortDateString()) <= DateTime.Parse(DateTime.Parse(endDate).ToShortDateString()));
                foreach (var secondStep in group)
                {
                    //adding values to DataTable
                    dataTable.Rows.Add(secondStep.SensorID, sensorName, secondStep.Timestamp, secondStep.Value);
                }

            }
            //Selecting only columns from DataTable
            string columns = string.Join(";", dataTable.Columns.OfType<DataColumn>().Select(x => string.Join(" ; ", x.ColumnName)));
            //Selecting only rows from datatable
            string rows = string.Join(Environment.NewLine, dataTable.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
            //Adding new line to variable columns - so after export there is new line after header with column names
            columns += Environment.NewLine;
            //adding rows to columns
            string output = columns + rows;
            //returning file after clicking on button Export
            return File(Encoding.ASCII.GetBytes(output), "text/csv", device.Name + "-" + DateTime.Now.ToShortDateString() + ".csv");
        }

        public string getValuesToGraph()
        {
            string json = System.Text.Json.JsonSerializer.Serialize(singleExportData.returnInstance().valuesToGraph);
            //string data = JsonConvert.SerializeObject(singleExportData.returnInstance().valuesToGraph);
            singleExportData.returnInstance().valuesToGraph.Clear();
            singleExportData.returnInstance().values.Clear();
            return json;
        }
    }
}
