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
    public class FacilitiesController : ControllerBase
    {
        private IFacilityService facilityService;

        public FacilitiesController(IFacilityService facilityService)
        {
            this.facilityService = facilityService;
        }

        // GET: api/Facilities
        [HttpGet]
        public IActionResult Get()
        {
            List<FacilityModel> facilityModels = new List<FacilityModel>();
            List<Facility> unists = facilityService.GetList();

            foreach (var facility in unists)
            {
                FacilityModel facilityModel = new FacilityModel();
                facilityModel.Id = facility.Id;
                facilityModel.ContractId = facility.ContractId;
                facilityModel.Code = facility.Code;
                facilityModel.Capacity = facility.Capacity;
                facilityModel.BreakdownFee = facility.BreakdownFee;
                facilityModel.Brand = facility.Brand;
                facilityModel.Address = facility.Address;
                facilityModel.CurrentMaintenanceFee = facility.CurrentMaintenanceFee;
                facilityModel.MaintenanceStatus = facility.MaintenanceStatus;
                facilityModel.OldMaintenanceFee = facility.OldMaintenanceFee;
                facilityModel.Name = facility.Name;
                facilityModel.Speed = facility.Speed;
                facilityModel.Station = facility.Station;
                facilityModel.Type = facility.Type;
                facilityModel.WarrantyFinishDate = facility.WarrantyFinishDate;

                facilityModels.Add(facilityModel);
            }

            return Ok(facilityModels);
        }

        // GET: api/Facilities/GetByFacilityId/5
        [Route("GetByFacilityId/{id}")]
        [HttpGet]
        public IActionResult GetByFacilityId(Guid id)
        {
            if (id != Guid.Empty)
            {
                Facility facility = facilityService.GetByFacilityId(id);
                FacilityModel facilityModel = new FacilityModel {Id = facility.Id ,ContractId = facility.ContractId, Code = facility.Code, Capacity = facility.Capacity, BreakdownFee = facility.BreakdownFee, Brand = facility.Brand, Address = facility.Address, CurrentMaintenanceFee = facility.CurrentMaintenanceFee, MaintenanceStatus = facility.MaintenanceStatus, OldMaintenanceFee = facility.OldMaintenanceFee, Name = facility.Name, Speed = facility.Speed, Station = facility.Station, Type = facility.Type, WarrantyFinishDate = facility.WarrantyFinishDate };

                return Ok(facilityModel);
            }

            return BadRequest();
        }

        // GET: api/Facilities/GetByClientContract/5
        [Route("GetByContractId/{contractId}")]
        [HttpGet]
        public IActionResult GetByContractId(Guid contractId)
        {
            List<FacilityModel> facilityModels = new List<FacilityModel>();
            List<Facility> unists = facilityService.GetByContractId(contractId);

            foreach (var facility in unists)
            {
                FacilityModel facilityModel = new FacilityModel();
                facilityModel.Id = facility.Id;
                facilityModel.ContractId = facility.ContractId;
                facilityModel.Code = facility.Code;
                facilityModel.Capacity = facility.Capacity;
                facilityModel.BreakdownFee = facility.BreakdownFee;
                facilityModel.Brand = facility.Brand;
                facilityModel.Address = facility.Address;
                facilityModel.CurrentMaintenanceFee = facility.CurrentMaintenanceFee;
                facilityModel.MaintenanceStatus = facility.MaintenanceStatus;
                facilityModel.OldMaintenanceFee = facility.OldMaintenanceFee;
                facilityModel.Name = facility.Name;
                facilityModel.Speed = facility.Speed;
                facilityModel.Station = facility.Station;
                facilityModel.Type = facility.Type;
                facilityModel.WarrantyFinishDate = facility.WarrantyFinishDate;

                facilityModels.Add(facilityModel);
            }

            return Ok(facilityModels);
        }

        // DELETE: api/Facilities/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                facilityService.Delete(id);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // POST: api/Facilities                    
        [HttpPost]
        public IActionResult Post([FromBody] FacilityModel facilityModel)
        {

            if (ModelState.IsValid)
            {

                Facility facility = new Facility { ContractId = facilityModel.ContractId, Code = facilityModel.Code, Capacity = facilityModel.Capacity, BreakdownFee = facilityModel.BreakdownFee, Brand = facilityModel.Brand, Address = facilityModel.Address, CurrentMaintenanceFee = facilityModel.CurrentMaintenanceFee, MaintenanceStatus = facilityModel.MaintenanceStatus, OldMaintenanceFee = facilityModel.OldMaintenanceFee, Name = facilityModel.Name, Speed = facilityModel.Speed, Station = facilityModel.Station, Type = facilityModel.Type, WarrantyFinishDate = facilityModel.WarrantyFinishDate };
                facilityService.Add(facility);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/Facilities/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] FacilityModel facilityModel)
        {
            if (ModelState.IsValid)
            {

                Facility facility = facilityService.GetByFacilityId(facilityModel.Id);
                facility.Id = facility.Id;
                facility.ContractId = facilityModel.ContractId;
                facility.Code = facilityModel.Code;
                facility.Capacity = facilityModel.Capacity;
                facility.BreakdownFee = facilityModel.BreakdownFee;
                facility.Brand = facilityModel.Brand;
                facility.Address = facilityModel.Address;
                facility.CurrentMaintenanceFee = facilityModel.CurrentMaintenanceFee;
                facility.MaintenanceStatus = facilityModel.MaintenanceStatus;
                facility.OldMaintenanceFee = facilityModel.OldMaintenanceFee;
                facility.Name = facilityModel.Name;
                facility.Speed = facilityModel.Speed;
                facility.Station = facilityModel.Station;
                facility.Type = facilityModel.Type;
                facility.WarrantyFinishDate = facilityModel.WarrantyFinishDate;
                facilityService.Update(facility);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}