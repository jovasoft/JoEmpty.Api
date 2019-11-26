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
    public class ContractController : ControllerBase
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

            if(contracts == null || contracts.Count == 0) return NotFound();

            List<ContractModel> contractModels = new List<ContractModel>();

            contracts.ForEach(x => { contractModels.Add(ContractModel.DtoToModel(x)); });

            return Ok(contractModels);
        }

        // GET: api/Contracts/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Contract contract = contractService.Get(id);

            if (contract == null) return NotFound();

            return Ok(ContractModel.DtoToModel(contract));
        }

        // GET: api/Contracts/GetByClientContracts/clientId
        [HttpGet("GetByClientContracts/{clientId}")]
        public IActionResult GetByClientContracts(Guid clientId)
        {
            List<Contract> contracts = contractService.GetList(clientId);

            if (contracts == null || contracts.Count == 0) return NotFound();

            List<ContractModel> contractModels = new List<ContractModel>();

            contracts.ForEach(x => { contractModels.Add(ContractModel.DtoToModel(x)); });

            return Ok(contractModels);
        }

        // POST: api/Contracts
        [HttpPost]
        public IActionResult Post([FromBody] ContractModel contractModel)
        {
            if (contractModel.ClientId == Guid.Empty) return NotFound();

            if (contractModel.StartDate > contractModel.FinishDate) return BadRequest();

            if (!string.IsNullOrEmpty(contractModel.Code))
            {
                Contract isExists = contractService.Get(contractModel.Code);

                if (isExists != null) return BadRequest();
            }

            Contract contract = ContractModel.ModelToDto(contractModel);
            contractService.Add(contract);

            ContractModel created = ContractModel.DtoToModel(contract);

            return CreatedAtAction(nameof(GetOne), new { created.Id }, created);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ContractModel contractModel)
        {
            if (id == Guid.Empty) return NotFound();

            Contract contract = contractService.Get(id);

            if(contract == null) return NotFound();

            if (Guid.Empty == contractModel.ClientId) contract.ClientId = contractModel.ClientId;
            if (!string.IsNullOrEmpty(contractModel.Code))
            {
                Contract isExists = contractService.Get(contractModel.Code);

                if (isExists != null && id != isExists.Id) return BadRequest();

                contract.Code = contractModel.Code;
            }

            if (contractModel.StartDate != null) contract.StartDate = contractModel.StartDate;
            if (contractModel.FinishDate != null) contract.FinishDate = contractModel.FinishDate;
            if (contractModel.FacilityCount > 0) contract.FacilityCount = contractModel.FacilityCount;
            if (contractModel.Currency > 0) contract.Currency = contractModel.Currency;
            if (contractModel.Supply > 0) contract.Supply = contractModel.Supply;
            if (contractModel.Amount > 0) contract.Amount = contractModel.Amount;

            if (contractModel.StartDate > contractModel.FinishDate) return BadRequest();

            contractService.Update(contract);

            ContractModel accepted = ContractModel.DtoToModel(contract);

            return Accepted(accepted);
        }

        // DELETE: api/BankAccount/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Contract contract = contractService.Get(id);

            if (contract == null) return NotFound();

            contractService.Delete(id);

            return NoContent();
        }
    }
}