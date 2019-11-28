using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Facility : IEntity
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public Guid AreaId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Station { get; set; }
        public int Speed { get; set; }
        public int Capacity { get; set; }
        public DateTime? WarrantyFinishDate { get; set; }
        public MaintenanceStatuses MaintenanceStatus { get; set; }
        public FacilityTypes Type { get; set; }
        public decimal CurrentMaintenanceFee { get; set; }
        public decimal OldMaintenanceFee { get; set; }
        public decimal BreakdownFee { get; set; }
        public string Brand { get; set; }
    }
}
