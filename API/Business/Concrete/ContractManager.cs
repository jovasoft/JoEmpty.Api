﻿using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ContractManager : IContractService
    {

        IContractDal contractDal;

        public ContractManager(IContractDal contractDal)
        {
            this.contractDal = contractDal;
        }

        public void Add(Contract contract)
        {
            contractDal.Add(contract);
        }

        public void Delete(Guid id)
        {
            contractDal.Delete(contractDal.Get(x => x.Id == id));
        }

        public Contract GetContract(Guid contractId)
        {
            return contractDal.Get(x => x.Id == contractId);
        }

        public List<Contract> GetCustomerContracts(Guid customerId)
        {
            return contractDal.GetList(x => x.CustomerId == customerId);
        }

        public List<Contract> GetList()
        {
            return contractDal.GetList();
        }

        public bool Update(Contract contract)
        {
            contractDal.Update(contract);

            return true;
        }
    }
}
