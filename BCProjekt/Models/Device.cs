using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.Models
{
    //this is called when user adds new meteostation
    public class Device
    {
        [Key]
        public int DevID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Minimum length of Name is 2 characters and maximum is 25.")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Minimum length of place is 5 characters and maximum is 50.")]
        public string Manufacturer { get; set; }

        public string LoadType { get; set; }

        public string Protocol { get; set; }

        public string Geolocation { get; set; }
        [Range(3,60)]
        public float TimeDelay { get; set; }

        public string Units { get; set; }
        //Address is used for listening or asking. I have to know, on what IP/url I want to listen to POST or I have to know on what IP/url I want to send GET requests
        public string Address { get; set; }
#nullable enable
        public string? UrlFormat { get; set; }
#nullable disable
        public string PrefferedCommunication { get; set; }
        public ICollection<Sensors> Sensors { get; set; }

        public User User { get; set; }
    }
}
