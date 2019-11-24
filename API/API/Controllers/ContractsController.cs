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
    public class ContractsController : ControllerBase
    {
        private IContractService contractService;

        public ContractsController(IContractService contractService)
        {
            this.contractService = contractService;
        }

        // GET: api/Contracts
        [HttpGet]
        public IActionResult Get()
        {
            List<ContractModel> contractModels = new List<ContractModel>();
            List<Contract> contracts = contractService.GetList();

            foreach (var contract in contracts)
            {
                ContractModel contractModel = new ContractModel();
                contractModel.Id = contract.Id;
                contractModel.ClientId = contract.ClientId;
                contractModel.Currency = contract.Currency;
                contractModel.Code = contract.Code;
                contractModel.Amount = contract.Amount;
                contractModel.StartDate = contract.StartDate;
                contractModel.FinishDate = contract.FinishDate;
                contractModel.FacilityCount = contract.FacilityCount;
                contractModel.Supply = contract.Supply;

                contractModels.Add(contractModel);
            }

            return Ok(contractModels);
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                Contract contract = contractService.GetContract(id);
                ContractModel contractModel = new ContractModel { Id = contract.Id, ClientId = contract.ClientId, Currency = contract.Currency, Code = contract.Code, Amount = contract.Amount, StartDate = contract.StartDate, FinishDate = contract.FinishDate, Supply = contract.Supply, FacilityCount = contract.FacilityCount };

                return Ok(contractModel);
            }

            return BadRequest();
        }

        // GET: api/Contracts/GetByClientContract/
        [Route("GetByClientContract/{clientId}")]
        [HttpGet]
        public IActionResult GetByClientContract(Guid clientId)
        {
            if (clientId != Guid.Empty)
            {
                List<Contract> contracts = contractService.GetClientContracts(clientId);
                List<ContractModel> contractModels = new List<ContractModel>();

                foreach (var contract in contracts)
                {
                    ContractModel contractModel = new ContractModel();
                    contractModel.Id = contract.Id;
                    contractModel.ClientId = contract.ClientId;
                    contractModel.Currency = contract.Currency;
                    contractModel.Code = contract.Code;
                    contractModel.Amount = contract.Amount;
                    contractModel.StartDate = contract.StartDate;
                    contractModel.FinishDate = contract.FinishDate;
                    contractModel.FacilityCount = contract.FacilityCount;
                    contractModel.Supply = contract.Supply;

                    contractModels.Add(contractModel);
                }

                return Ok(contractModels);
            }

            return BadRequest();
        }

        // DELETE: api/BankAccount/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                contractService.Delete(id);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // POST: api/Contracts                    
        [HttpPost]
        public IActionResult Post([FromBody] ContractModel contractModel)
        {

            if (ModelState.IsValid)
            {

                Contract contract = new Contract { ClientId = contractModel.ClientId, Currency = contractModel.Currency, Code = contractModel.Code, Amount = contractModel.Amount, StartDate = contractModel.StartDate, FinishDate = contractModel.FinishDate, Supply = contractModel.Supply, FacilityCount = contractModel.FacilityCount };
                contractService.Add(contract);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ContractModel contractModel)
        {
            if (ModelState.IsValid)
            {

                Contract contract = contractService.GetContract(contractModel.Id);
                contract.Id = contractModel.Id;
                contract.ClientId = contractModel.ClientId;
                contract.Currency = contractModel.Currency;
                contract.Code = contractModel.Code;
                contract.Amount = contractModel.Amount;
                contract.StartDate = contractModel.StartDate;
                contract.FinishDate = contractModel.FinishDate;
                contract.FacilityCount = contractModel.FacilityCount;
                contract.Supply = contractModel.Supply;
                contractService.Update(contract);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}