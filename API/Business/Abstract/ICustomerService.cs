using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        void Add(Customer customer);

        Customer Get(Guid id);

        Customer Get(string currentCode);

        void Delete(string currentCode);

        bool Update(Customer customer);

        List<Customer> GetList();
    }
}
