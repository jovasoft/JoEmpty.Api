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

        List<Contract> GetClientContracts(Guid clientId);

        void Delete(Guid contractId);

        bool Update(Contract contract);

        List<Contract> GetList();
    }
}
