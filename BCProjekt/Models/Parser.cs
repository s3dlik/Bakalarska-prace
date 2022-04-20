using System;

namespace BC.Models
{
    public class Parser
    {
        public string serial_number { get; set; }
        public string model { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string place { get; set; }
        public string value { get; set; }
        public Parser(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public Parser(string type, string name, string value)
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }
        public Parser(string serNum, string model, string time, string date) {
            this.serial_number = serNum;
            this.model = model;
            this.time = time;
            this.date = date;
        }
    }
}
