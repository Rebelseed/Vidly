using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.ViewModels;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContextViewModel _context;

        public CustomersController()
        {
            _context = new ApplicationDbContextViewModel();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customersDtos = _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customersDtos);
        }

        // GET /api/customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/id
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerinDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDB == null)
                return NotFound();

            Mapper.Map(customerDto, customerinDB);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/id
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerinDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDB == null)
                return NotFound();

            _context.Customers.Remove(customerinDB);
            _context.SaveChanges();

            return Ok();
        }
    }
}
