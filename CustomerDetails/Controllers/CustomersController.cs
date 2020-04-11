using CustomerDetails.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CustomerDetails.Controllers
{
    public class CustomersController : Controller
    {
        private CustomerDbContext _context;
        public CustomersController()
        {
            _context = new CustomerDbContext();
        }
        public ViewResult Index()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customer
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View("New",customer);
            
        }
        public ActionResult New()
        {
            return View();
        }

       [HttpPost]
       //[ValidateAntiForgeryToken]    //avoid CSRF
        public ActionResult Save(Customer customerModel)
        {
            if (customerModel.Id == 0)  // Add new customer
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseuri"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Customer>("customers", customerModel);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Customers");
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())   // update new customer
                {
                    client.BaseAddress = new Uri("http://localhost:52101/api/customers");

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<Customer>("customers", customerModel);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }               
            }
            return View(customerModel);
        }

    }
}