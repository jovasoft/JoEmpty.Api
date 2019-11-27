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

            if (facilities == null || facilities.Count == 0) return Errors("Tesis listesi bulunamadı.", null, 404);

            List<FacilityModel> facilityModels = new List<FacilityModel>();

            facilities.ForEach(x => { facilityModels.Add(FacilityModel.DtoToModel(x)); });

            return Success(null, facilityModels, 200);
        }

        // GET: api/Facility/GetByFacilityId/id
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı tesis yok.", null, 404);

            Facility facility = facilityService.Get(id);

            if (facility == null) return Errors("Böyle bir ID numaralı tesis yok.", null, 404);

            return Success(null, FacilityModel.DtoToModel(facility));
        }

        // GET: api/Facility/GetByContractFacilities/contractId
        [HttpGet("GetByContractFacilities/{contractId}")]
        public IActionResult GetByContractFacilities(Guid contractId)
        {
            if (contractId == Guid.Empty) return Errors("Böyle bir ID numaralı sözleşmenin tesisleri yok.", null, 404);

            List<Facility> facilities = facilityService.GetList(contractId);

            if (facilities == null || facilities.Count == 0) return Errors("Böyle bir ID numaralı sözleşmenin tesisleri yok.", null, 404);

            List<FacilityModel> facilityModels = new List<FacilityModel>();

            facilities.ForEach(x => { facilityModels.Add(FacilityModel.DtoToModel(x)); });

            return Success(null, facilityModels, 200);
        }

        // POST: api/Facility
        [HttpPost]
        public IActionResult Post([FromBody] FacilityModel facilityModel)
        {
            if (facilityModel.ContractId == Guid.Empty) return Errors("Sözleşme ID boş olamaz.", null, 404);

            if (!string.IsNullOrEmpty(facilityModel.Code))
            {
                Facility isExists = facilityService.Get(facilityModel.Code);

                if (isExists != null) return Errors("Bu tesis koduna ait kayıt vardır.", null);
            }

            Contract contract = contractService.Get(facilityModel.ContractId);

            if (contract == null) return Errors("Bu sözleşme koduna ait sözleşme olmadığı için tesis eklenemedi.", null, 404);

            int facilityCount = facilityService.GetList(facilityModel.ContractId).Count;

            if (facilityCount == contract.FacilityCount) return Errors("Bu sözleşmeye daha fazla tesis eklenemez.", null);

            Facility facility = FacilityModel.ModelToDto(facilityModel);
            facilityService.Add(facility);

            if (facilityService.Get(facility.Id) == null) return Errors("Bölge kaydı yapılamadı.", null, 404);

            return Success(null, FacilityModel.DtoToModel(facility), 201);
        }

        // PUT: api/Facility/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] FacilityModel facilityModel)
        {
            if (id == Guid.Empty) return Errors("Tesis ID boş olamaz.", null, 404);

            Facility facility = facilityService.Get(id);

            if (facility == null) return Errors("Güncellenmek istenen tesis bulunamadı.", null, 404);

            if (!string.IsNullOrEmpty(facilityModel.Address)) facility.Address = facilityModel.Address;
            if (!string.IsNullOrEmpty(facilityModel.Brand)) facility.Brand = facilityModel.Brand;
            if (!string.IsNullOrEmpty(facilityModel.Code))
            {
                Facility isExists = facilityService.Get(facilityModel.Code);

                if (isExists != null && id != isExists.Id) return Errors("Güncellenmek istenen tesis kodu başka bir tesise atanmıştır.", null);

                facility.Code = facilityModel.Code;
            }
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

            return Success(null, FacilityModel.DtoToModel(facility), 202);
        }

        // DELETE: api/Facility/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı tesis yok.", null, 404);

            Facility facility = facilityService.Get(id);

            if (facility == null) return Errors("Böyle bir ID numaralı tesis yok.", null, 404);

            facilityService.Delete(id);

            return Success(null, null, 204);
        }
    }
}