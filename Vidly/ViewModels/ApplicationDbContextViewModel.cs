using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.ViewModels
{
    public class ApplicationDbContextViewModel : IdentityDbContext<ApplicationUserViewModel>

    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }


        public ApplicationDbContextViewModel()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
        }

        public static ApplicationDbContextViewModel Create()
        {
            return new ApplicationDbContextViewModel();
        }
    }
}