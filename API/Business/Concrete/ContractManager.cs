using Business.Abstract;
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

        public Contract Get(Guid contractId)
        {
            return contractDal.Get(x => x.Id == contractId);
        }

        public List<Contract> GetList(Guid clientId)
        {
            return contractDal.GetList(x => x.ClientId == clientId);
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
