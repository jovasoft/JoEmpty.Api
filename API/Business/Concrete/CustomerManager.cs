using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            this.customerDal = customerDal;
        }
        public void Add(Customer customer)
        {
            customerDal.Add(customer);
        }

        public void Delete(string currentCode)
        {
            customerDal.Delete(customerDal.Get(x => x.CurrentCode == currentCode));
        }

        public Customer Get(Guid id)
        {
            return customerDal.Get(x => x.Id == id);
        }

        public Customer Get(string currentCode)
        {
            return customerDal.Get(x => x.CurrentCode == currentCode);
        }

        public List<Customer> GetList()
        {
            return customerDal.GetList();
        }

        public bool Update(Customer customer)
        {
            customerDal.Update(customer);

            return true;
        }
    }
}
