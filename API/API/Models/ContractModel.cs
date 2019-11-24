using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ContractModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int FacilityCount { get; set; }
        public Currencies Currency { get; set; }
        public Supplies Supply { get; set; }
        public decimal Amount { get; set; }
    }
}
