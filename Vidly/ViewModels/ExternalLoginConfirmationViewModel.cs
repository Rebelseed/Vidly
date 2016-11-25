using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.ViewModels;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Driving Licence")]
        public string DriverLicence { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
    }
}