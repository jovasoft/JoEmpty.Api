using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ResponseController
    {
        private IContractService contractService;

        public ContractController(IContractService contractService)
        {
            this.contractService = contractService;
        }

        // GET: api/Contracts
        [HttpGet]
        public IActionResult Get()
        {
            List<Contract> contracts = contractService.GetList();

            if(contracts == null || contracts.Count == 0) return Error("Eşleşen kayıt bulunamadı.", null, 404);

            List<ContractModel> contractModels = new List<ContractModel>();

            contracts.ForEach(x => { contractModels.Add(ContractModel.DtoToModel(x)); });

            return Success(null, contractModels, 200);
        }

        // GET: api/Contracts/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Error("Sözleşme bulunamadı.", null, 404);

            Contract contract = contractService.Get(id);

            if (contract == null) return Error("Sözleşme bulunamadı.", null, 404);

            return Success(null, ContractModel.DtoToModel(contract));
        }

        // GET: api/Contracts/GetByClientContracts/clientId
        [HttpGet("GetByClientContracts/{clientId}")]
        public IActionResult GetByClientContracts(Guid clientId)
        {
            if (clientId == Guid.Empty) return Error("Müşteri bulunamadı.", null, 404);

            List<Contract> contracts = contractService.GetList(clientId);

            if (contracts == null || contracts.Count == 0) return Error("Eşleşen kayıt bulunamadı.", null, 404);

            List<ContractModel> contractModels = new List<ContractModel>();

            contracts.ForEach(x => { contractModels.Add(ContractModel.DtoToModel(x)); });

            return Success(null, contractModels, 200);
        }

        // POST: api/Contracts
        [HttpPost]
        public IActionResult Post([FromBody] ContractModel contractModel)
        {
            if (contractModel.ClientId == Guid.Empty) return Error("Müşteri bulunamadı.", null, 404);

            if (contractModel.StartDate > contractModel.FinishDate) return Error("Sözleşme bitiş tarihi başlangıç tarihinden küçük olamaz.", null);

            if (!string.IsNullOrEmpty(contractModel.Code))
            {
                Contract isExists = contractService.Get(contractModel.Code);

                if (isExists != null) return Error("Bu koda ait bir sözleşme zaten var.", null);
            }

            Contract contract = ContractModel.ModelToDto(contractModel);
            contractService.Add(contract);

            if (contractService.Get(contract.Id) == null) return Error("Sözleşme eklenemedi.", null);

            return Success(null, ContractModel.DtoToModel(contract), 201);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ContractModel contractModel)
        {
            if (id == Guid.Empty) return Error("Sözleşme bulunamadı.", null, 404);

            Contract contract = contractService.Get(id);

            if(contract == null) return Error("Sözleşme bulunamadı.", null, 404);

            if (Guid.Empty == contractModel.ClientId) contract.ClientId = contractModel.ClientId;
            if (!string.IsNullOrEmpty(contractModel.Code))
            {
                Contract isExists = contractService.Get(contractModel.Code);

                if (isExists != null && id != isExists.Id) return Error("Güncellenmek istenen kod başka bir sözleşmeye ait.", null);

                contract.Code = contractModel.Code;
            }

            if (contractModel.StartDate != null) contract.StartDate = contractModel.StartDate;
            if (contractModel.FinishDate != null) contract.FinishDate = contractModel.FinishDate;
            if (contractModel.FacilityCount > 0) contract.FacilityCount = contractModel.FacilityCount;
            if (contractModel.Currency > 0) contract.Currency = contractModel.Currency;
            if (contractModel.Supply > 0) contract.Supply = contractModel.Supply;
            if (contractModel.Amount > 0) contract.Amount = contractModel.Amount;

            if (contractModel.StartDate > contractModel.FinishDate) return Error("Sözleşme bitiş tarihi başlangıç tarihinden küçük olamaz.", null);

            contractService.Update(contract);

            return Success(null, ContractModel.DtoToModel(contract), 202);
        }

        // DELETE: api/BankAccount/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Error("Sözleşme bulunamadı.", null, 404);

            Contract contract = contractService.Get(id);

            if (contract == null) return Error("Sözleşme bulunamadı.", null, 404);

            contractService.Delete(id);

            return Success(null, null, 204);
        }
    }
}