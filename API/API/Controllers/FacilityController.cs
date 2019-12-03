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
    public class FacilityController : ResponseController
    {
        private IFacilityService facilityService;
        private IContractService contractService;

        public FacilityController(IFacilityService facilityService, IContractService contractService)
        {
            this.facilityService = facilityService;
            this.contractService = contractService;
        }

        // GET: api/Facilities
        [HttpGet]
        public IActionResult Get()
        {
            List<Facility> facilities = facilityService.GetList();

            if (facilities == null || facilities.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<FacilityModel> facilityModels = new List<FacilityModel>();

            facilities.ForEach(x => { facilityModels.Add(FacilityModel.DtoToModel(x)); });

            return Success(facilityModels);
        }

        // GET: api/Facility/GetByFacilityId/id
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Error("Tesis bulunamadı.", 404);

            Facility facility = facilityService.Get(id);

            if (facility == null) return Error("Tesis bulunamadı.", 404);

            return Success(FacilityModel.DtoToModel(facility));
        }

        // GET: api/Facility/GetFacilitiesByContract/contractId
        [HttpGet("GetFacilitiesByContract/{contractId}")]
        public IActionResult GetFacilitiesByContract(Guid contractId)
        {
            if (contractId == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            List<Facility> facilities = facilityService.GetList(contractId);

            if (facilities == null || facilities.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<FacilityModel> facilityModels = new List<FacilityModel>();

            facilities.ForEach(x => { facilityModels.Add(FacilityModel.DtoToModel(x)); });

            return Success(facilityModels);
        }

        // GET: api/Facility/GetFacilitiesByClient/contractId
        [HttpGet("GetFacilitiesByClient/{clientId}")]
        public IActionResult GetFacilitiesByClient(Guid clientId)
        {
            if (clientId == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            List<Facility> facilities = facilityService.GetFacilitiesByClient(clientId);

            if (facilities == null || facilities.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<FacilityModel> facilityModels = new List<FacilityModel>();

            facilities.ForEach(x => { facilityModels.Add(FacilityModel.DtoToModel(x)); });

            return Success(facilityModels);
        }

        // POST: api/Facility
        [HttpPost]
        public IActionResult Post([FromBody] FacilityModel facilityModel)
        {
            if (facilityModel.ContractId == Guid.Empty) return Error("Sözleşme bulunamadı.", 404);

            if (!string.IsNullOrEmpty(facilityModel.Code))
            {
                Facility isExists = facilityService.Get(facilityModel.Code);

                if (isExists != null) return Error("Bu koda ait bir tesis zaten var.");
            }

            Contract contract = contractService.Get(facilityModel.ContractId);

            if (contract == null) return Error("Sözleşme bulunamadı.", 404);

            int facilityCount = facilityService.GetList(facilityModel.ContractId).Count;

            if (facilityCount == contract.FacilityCount) return Error("Bu sözleşmeye daha fazla tesis eklenemez.");

            Facility facility = FacilityModel.ModelToDto(facilityModel);
            facilityService.Add(facility);

            if (facilityService.Get(facility.Id) == null) return Error("Tesis eklenemedi.");

            return Success(FacilityModel.DtoToModel(facility), 201);
        }

        // PUT: api/Facility/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] FacilityModel facilityModel)
        {
            if (id == Guid.Empty) return Error("Tesis bulunamadı.", 404);

            Facility facility = facilityService.Get(id);

            if (facility == null) return Error("Tesis bulunamadı.", 404);

            if (!string.IsNullOrEmpty(facilityModel.Address)) facility.Address = facilityModel.Address;
            if (!string.IsNullOrEmpty(facilityModel.Province)) facility.Province = facilityModel.Province;
            if (!string.IsNullOrEmpty(facilityModel.District)) facility.District = facilityModel.District;
            if (!string.IsNullOrEmpty(facilityModel.Brand)) facility.Brand = facilityModel.Brand;
            if (!string.IsNullOrEmpty(facilityModel.Name)) facility.Name = facilityModel.Name;
            if (!string.IsNullOrEmpty(facilityModel.Code))
            {
                Facility isExists = facilityService.Get(facilityModel.Code);

                if (isExists != null && id != isExists.Id) return Error("Güncellenmek istenen kod başka bir tesise ait.");

                facility.Code = facilityModel.Code;
            }
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

            return Success(FacilityModel.DtoToModel(facility), 202);
        }

        // DELETE: api/Facility/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Error("Tesis bulunamadı.", 404);

            Facility facility = facilityService.Get(id);

            if (facility == null) return Error("Tesis bulunamadı.", 404);

            facilityService.Delete(id);

            return Success(null, 204);
        }
    }
}
