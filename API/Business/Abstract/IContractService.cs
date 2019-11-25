using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IContractService
    {
        void Add(Contract contract);

        Contract Get(Guid contractId);

        List<Contract> GetList(Guid clientId);

        void Delete(Guid contractId);

        bool Update(Contract contract);

        List<Contract> GetList();
    }
}
