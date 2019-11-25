using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class FacilityModel
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public Guid AreaId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public FacilityTypes Type { get; set; }

        [Required]
        public decimal CurrentMaintenanceFee { get; set; }

        [Required]
        public decimal BreakdownFee { get; set; }

        public int Station { get; set; }
        public int Speed { get; set; }
        public int Capacity { get; set; }
        public DateTime? WarrantyFinishDate { get; set; }
        public MaintenanceStatuses MaintenanceStatus { get; set; }
        public decimal OldMaintenanceFee { get; set; }
        public string Brand { get; set; }

        public static FacilityModel DtoToModel(Facility facility)
        {
            return new FacilityModel
            {
                Address = facility.Address,
                AreaId = facility.AreaId,
                Brand = facility.Brand,
                BreakdownFee = facility.BreakdownFee,
                Capacity = facility.Capacity,
                Code = facility.Code,
                ContractId = facility.ContractId,
                CurrentMaintenanceFee = facility.CurrentMaintenanceFee,
                Id = facility.Id,
                MaintenanceStatus = facility.MaintenanceStatus,
                Name = facility.Name,
                OldMaintenanceFee = facility.OldMaintenanceFee,
                Speed = facility.Speed,
                Station = facility.Station,
                Type = facility.Type,
                WarrantyFinishDate = facility.WarrantyFinishDate
            };
        }

        public static Facility ModelToDto(FacilityModel facilityModel)
        {
            return new Facility
            {
                Id = new Guid(),
                Address = facilityModel.Address,
                AreaId = facilityModel.AreaId,
                Brand = facilityModel.Brand,
                BreakdownFee = facilityModel.BreakdownFee,
                Capacity = facilityModel.Capacity,
                Code = facilityModel.Code,
                ContractId = facilityModel.ContractId,
                CurrentMaintenanceFee = facilityModel.CurrentMaintenanceFee,
                MaintenanceStatus = facilityModel.MaintenanceStatus,
                Name = facilityModel.Name,
                OldMaintenanceFee = facilityModel.OldMaintenanceFee,
                Speed = facilityModel.Speed,
                Station = facilityModel.Station,
                Type = facilityModel.Type,
                WarrantyFinishDate = facilityModel.WarrantyFinishDate
            };
        }
    }
}
