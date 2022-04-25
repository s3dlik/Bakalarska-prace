using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Login is required")]
        [StringLength(20,MinimumLength =4, ErrorMessage ="Login must be long between 4 and 20 characters.")]
        public string Login { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="Email has to be included.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage ="Email is not valid. Please input correct format of email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Password is required")]
        [RegularExpression(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage ="Your password doesnt meet the criteria. Password must have at least one digit, at lest one lowercase character, at least one uppercase character, at least one special character and it has to be long between 8 and 32 characters. For example: P@ssword1 ")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [RegularExpression(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Your password doesnt meet the criteria. Password must have at least one digit, at lest one lowercase character, at least one uppercase character, at least one special character and it has to be long between 8 and 32 characters. For example: P@ssword1 ")]
        [Compare("Password",ErrorMessage ="Passwords does not match!")]
        [Display(Name = "Password verify")]
        [DataType(DataType.Password)]
        public string PasswordCheck { get; set; }

        [Display(Name = "Date of registration")]
        public DateTime Registered { get; set; }
        [Display(Name ="Last login")]
        public DateTime LastLogin { get; set; }
        [ValidateNever]
        public ICollection<Device> Devices { get; set; }
    }
}
