using System;
using System.ComponentModel.DataAnnotations;

namespace BC.Models
{
    public class Values
    {
        [Key]
        public int ValID { get; set; }
        public string Value { get; set; }
        public string Timestamp { get; set; }
        public int SensorID { get; set; }
    }
}
