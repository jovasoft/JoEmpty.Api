using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerContactManager : ICustomerContactService
    {
        ICustomerContactDal customerContactDal;

        public CustomerContactManager(ICustomerContactDal customerContactDal)
        {
            this.customerContactDal = customerContactDal;
        }

        public void Add(CustomerContact customerContact)
        {
            customerContactDal.Add(customerContact);
        }

        public void Delete(Guid customerId)
        {
            customerContactDal.Delete(customerContactDal.Get(x => x.CustomerId == customerId));
        }

        public CustomerContact Get(Guid customerId)
        {
            return customerContactDal.Get(x => x.CustomerId == customerId);
        }

        public List<CustomerContact> GetList()
        {
            return customerContactDal.GetList();
        }

        public bool Update(CustomerContact customerContact)
        {
            customerContactDal.Update(customerContact);

            return true;
        }
    }
}
