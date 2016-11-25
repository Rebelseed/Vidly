using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Vidly.ViewModels
{
    public class ApplicationUserViewModel : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string DriverLicence { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUserViewModel> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}