using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BC.Models
{
    public class Sensors
    {
        [Key]
        public int SenID { get; set; }
        public string Name { get; set; }
        public Device Device { get; set; }
    }
}
