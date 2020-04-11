using CustomerDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerDetails.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private CustomerDbContext _context;
        CustomersController()
        {
            _context = new CustomerDbContext();
        }
        //api/customers
        public IEnumerable<Customer> GetCustomers()
        {
           return _context.Customers.ToList();
        }
        //api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer =  _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        //POST api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/"+ customer.Id),customer);
        }
        //PUT api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
      
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);
            if (customerInDb == null)
                return NotFound();

            customerInDb.FirstName = customer.FirstName;
            customerInDb.LastName = customer.LastName;
            customerInDb.Email = customer.Email;
            customerInDb.PhoneNumber = customer.PhoneNumber;

            _context.SaveChanges();
            return Ok();
        }
        //Delete customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerInDb); // removed from memory
            _context.SaveChanges();
        }
    }
}
