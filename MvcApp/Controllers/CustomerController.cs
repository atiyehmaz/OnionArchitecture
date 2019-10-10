using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Repsitory;
using Domain;
using Service;

namespace MvcApp.Controllers
{
    public class CustomerController : Controller
    {
        private IUnitOfWork<BankDbContext> unitOfWork;
        private IRepository<Customer> repository;
        private ICustomerService customerService;

        public CustomerController(IUnitOfWork<BankDbContext> unitOfWork, IRepository<Customer> repository
                                  , ICustomerService customerService)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.customerService = customerService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}