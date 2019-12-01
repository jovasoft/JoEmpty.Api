using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ContractModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? FinishDate { get; set; }

        [Required]
        public int FacilityCount { get; set; }

        [Required]
        [Range(1, 3)]
        public Currencies Currency { get; set; }

        [Required]
        [Range(1, 2)]
        public Supplies Supply { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string FormattedStartDate { get { return StartDate.Value.ToString("dd MMMM yyyy"); } }

        public string FormattedFinishDate { get { return FinishDate.Value.ToString("dd MMMM yyyy"); } }

        public static ContractModel DtoToModel(Contract contract)
        {
            return new ContractModel
            {
                Id = contract.Id,
                ClientId = contract.ClientId,
                Currency = contract.Currency,
                Code = contract.Code,
                StartDate = contract.StartDate,
                FinishDate = contract.FinishDate,
                FacilityCount = contract.FacilityCount,
                Supply = contract.Supply,
                Amount = contract.Amount
            };
        }

        public static Contract ModelToDto(ContractModel contractModel)
        {
            return new Contract
            {
                Id = Guid.NewGuid(),
                ClientId = contractModel.ClientId,
                Currency = contractModel.Currency,
                Code = contractModel.Code,
                StartDate = contractModel.StartDate,
                FinishDate = contractModel.FinishDate,
                FacilityCount = contractModel.FacilityCount,
                Supply = contractModel.Supply,
                Amount = contractModel.Amount
            };
        }
    }
}
