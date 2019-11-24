using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IContractService
    {
        void Add(Contract contract);

        Contract GetContract(Guid contractId);

        List<Contract> GetCustomerContracts(Guid customerId);

        void Delete(Guid contractId);

        bool Update(Contract contract);

        List<Contract> GetList();
    }
}
