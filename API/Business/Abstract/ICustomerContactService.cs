using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerContactService
    {
        void Add(CustomerContact customerContact);

        CustomerContact Get(Guid customerId);

        void Delete(Guid customerId);

        bool Update(CustomerContact customerContact);

        List<CustomerContact> GetList();
    }
}
