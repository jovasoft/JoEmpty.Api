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
            List<Facility> facilities = facilityService.GetList();

            if (facilities == null || facilities.Count == 0) return NotFound();

            List<FacilityModel> facilityModels = new List<FacilityModel>();

            facilities.ForEach(x => { facilityModels.Add(FacilityModel.DtoToModel(x)); });

            return Ok(facilityModels);
        }

        // GET: api/Facilities/GetByFacilityId/id
        [HttpGet("{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Facility facility = facilityService.Get(id);

            if (facility == null) return NotFound();

            return Ok(FacilityModel.DtoToModel(facility));
        }

        // GET: api/Facilities/GetByContractFacilities/contractId
        [HttpGet("{contractId}")]
        public IActionResult GetByContractFacilities(Guid contractId)
        {
            if (contractId == Guid.Empty) return NotFound();

            List<Facility> facilities = facilityService.GetList(contractId);

            if (facilities == null || facilities.Count == 0) return NotFound();

            List<FacilityModel> facilityModels = new List<FacilityModel>();

            facilities.ForEach(x => { facilityModels.Add(FacilityModel.DtoToModel(x)); });

            return Ok(facilityModels);
        }

        // POST: api/Facilities
        [HttpPost]
        public IActionResult Post([FromBody] FacilityModel facilityModel)
        {
            if (facilityModel.ContractId == Guid.Empty) return NotFound();

            Facility facility = FacilityModel.ModelToDto(facilityModel);
            facilityService.Add(facility);

            FacilityModel created = FacilityModel.DtoToModel(facility);

            return CreatedAtAction(nameof(GetOne), new { created.Id }, created);
        }

        // PUT: api/Facilities/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] FacilityModel facilityModel)
        {
            if (id == Guid.Empty) return NotFound();

            Facility facility = facilityService.Get(id);

            if (facility == null) return NotFound();

            if (!string.IsNullOrEmpty(facilityModel.Address)) facility.Address = facilityModel.Address;
            if (!string.IsNullOrEmpty(facilityModel.Brand)) facility.Brand = facilityModel.Brand;
            if (!string.IsNullOrEmpty(facilityModel.Code)) facility.Code = facilityModel.Code;
            if (!string.IsNullOrEmpty(facilityModel.Name)) facility.Name = facilityModel.Name;
            if (facilityModel.WarrantyFinishDate != null) facility.WarrantyFinishDate = facilityModel.WarrantyFinishDate;
            if (facilityModel.AreaId != Guid.Empty) facility.AreaId = facilityModel.AreaId;
            if (facilityModel.ContractId != Guid.Empty) facility.ContractId = facilityModel.ContractId;
            if (facilityModel.BreakdownFee > 0) facility.BreakdownFee = facilityModel.BreakdownFee;
            if (facilityModel.Capacity > 0) facility.Capacity = facilityModel.Capacity;
            if (facilityModel.CurrentMaintenanceFee > 0) facility.CurrentMaintenanceFee = facilityModel.CurrentMaintenanceFee;
            if (facilityModel.MaintenanceStatus > 0) facility.MaintenanceStatus = facilityModel.MaintenanceStatus;
            if (facilityModel.OldMaintenanceFee > 0) facility.OldMaintenanceFee = facilityModel.OldMaintenanceFee;
            if (facilityModel.Speed > 0) facility.Speed = facilityModel.Speed;
            if (facilityModel.Station > 0) facility.Station = facilityModel.Station;
            if (facilityModel.Type > 0) facility.Type = facilityModel.Type;

            facilityService.Update(facility);

            return Accepted(facilityModel);
        }

        // DELETE: api/Facilities/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            facilityService.Delete(id);

            return NoContent();
        }
    }
}