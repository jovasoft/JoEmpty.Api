using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Contract : IEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int UnitCount { get; set; }
        public Currencies Currency { get; set; }
        public Supplies Supply { get; set; }
        public decimal Amount { get; set; }
    }
}
