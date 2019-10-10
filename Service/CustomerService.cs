using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data.Repsitory;
using Data;

namespace Service
{
    public class CustomerService :Repository<Customer>, ICustomerService
    {
        private readonly IRepository<Customer> customerRepository;
        //private readonly IUnitOfWork<BankDbContext> unitOfWork;

        //public CustomerService(IRepository<Customer> customerRepository,
        //                       IUnitOfWork<BankDbContext> unitOfWork)
        //{
        //    this.customerRepository = customerRepository;
        //    this.unitOfWork = unitOfWork;
        //}

        public CustomerService(IUnitOfWork<BankDbContext> unitOfWork,
                               IRepository<Customer> customerRepository)
                              : base(unitOfWork)
        {
            this.customerRepository = customerRepository;
        }
        public CustomerService(BankDbContext context, IRepository<Customer> customerRepository)
                              : base(context)
        {
            this.customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return customerRepository.GetAll();
        }

        public Customer GetById(Customer id)
        {
            return customerRepository.GetById(id);
        }
        
        public new void Insert(Customer customer)
        {
            customerRepository.Insert(customer);
        }

        public new void Delete(Customer customer)
        {
            customerRepository.Delete(customer);
        }

        //public void Save()
        //{
        //    customerRepository.Save();
        //}

        public new void Update(Customer customer)
        {
            customerRepository.Update(customer);
        }

       
    }
}
