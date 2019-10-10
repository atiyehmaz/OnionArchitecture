using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Data.Repsitory;
using Domain;
using Service;
using Data;

namespace WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IUnitOfWork<BankDbContext> _unitOfWork;
        //private readonly IRepository<Customer> _repository;
        private readonly ICustomerService _customerService;

        public CustomerController(IUnitOfWork<BankDbContext> unitOfWork/*, IRepository<Customer> repository,*/,
                                   ICustomerService customerService)
        {
            this._unitOfWork = unitOfWork;
            // this._repository = repository;
            this._customerService = customerService;
        }



        [HttpGet]
        public IHttpActionResult GetCustomers()

        {
            var list = _customerService.GetCustomers();
            return Ok(list);
        }

        [HttpPost]
        [Route("api/Customer/CreateCustomer")]
        public IHttpActionResult CreateCustomer([FromBody]Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    _customerService.Insert(customer);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                    if (_unitOfWork.Successful==true)
                    {
                        return Ok();
                    }

                }
                catch (Exception)
                {

                    _unitOfWork.Rollback();
                    return BadRequest();
                }

            }
            return BadRequest();
        }
    }
}