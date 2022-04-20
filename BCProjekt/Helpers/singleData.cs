using System.Collections.Generic;
using BC.Models;

namespace BC.Helpers
{
    
    public class singleData
    {
        public List<Parser> dataList;
        public List<Sensors> deviceSensors;
        public string Communicaton { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }
        public string Protocol { get; set; }
        public string TimeDelay { get; set; }
        public string Unit { get; set; }
#nullable enable
        public string? URLFormat { get; set; }
#nullable disable
        private static singleData instance = null;
        public singleData()
        {
            this.dataList = new List<Parser>();
            this.deviceSensors = new List<Sensors>();
            this.Communicaton = string.Empty;
            this.DataType = string.Empty;
            this.Protocol = string.Empty;
            this.Unit = string.Empty;
            this.TimeDelay = string.Empty;
            this.URLFormat = string.Empty;
        }
        
        public static singleData returnInstance() {
            if (instance == null) {
                instance = new singleData();
            }
            return instance; 
        }
    }
    
}
