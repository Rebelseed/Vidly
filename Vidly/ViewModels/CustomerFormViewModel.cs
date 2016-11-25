﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.ViewModels;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
        public string Title
        {
            get
            {
                if (Customer != null && Customer.Id != 0)
                    return "Edit Customer";

                else
                    return "Add Customer";
            }
        }
    }
}