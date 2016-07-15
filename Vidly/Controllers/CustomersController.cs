using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using Vidly.Migrations;

namespace Vidly.Controllers
{
    /**
     * Controller used to navigate to different customer views as well as ActionResults
     * for Customer CRUD.
     * */
    public class CustomersController : Controller
    {
        //Application context for access database
        private ApplicationDbContext _context;

        //Default constructor. Instantiates new instance of application context.
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /**
         * ActionResult for directing to customer form to enter a new employee.
         * Provides an initialized CustomerFormViewModel that gets passed to the
         * CustomerForm view.
         * */
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }


        /**
         * HttpPost request for saving a new customer or updating a customer to the database.
         * Given a Customer object from the view. Checks if model is valid. If not returns to 
         * CustomerForm with fields populated. If valid and Id is 0, adds customer to database.
         * If valid and Id is not 0, updates customer fields and updates database.
         * Returns to customer index page after successfully adding or updating to database.
         * */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        /**
         * ActionResult for editing an existing customer. Given a customer id from the view and
         * searches the database. If not found displays HttpNotFound page.
         * If found creates a new CustomerFormViewModel with values from customer table where id equals
         * id from view. Displays CustomerForm with populated fields to be edited.
         * */
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        /**
         * ActionResult for directing to customer index page that displays a list of customers
         * currently in the system. 
         * */
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }


        /**
         * ActionResult for directing to a customer's detailed information. Given a customer id from
         * the view and searches the database. If not found displays HttpNotFound page.
         * If found directed to customer's details page.
         * */
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
    }
}