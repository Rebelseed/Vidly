using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext;
        }

        // GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/id
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        // POST /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            else
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // PUT /api/customers/id
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

           
                var customerinDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            else
                customerinDB.Name = customer.Name;
                customerinDB.BirthDate = customer.BirthDate;
                customerinDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerinDB.MembershipTypeId = customer.MembershipTypeId;

                _context.SaveChanges();
        }

        // DELETE /api/customers/id
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerinDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            else
                _context.Customers.Remove(customerinDB);
                _context.SaveChanges();
        }
    }
}
