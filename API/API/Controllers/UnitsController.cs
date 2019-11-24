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
    public class UnitsController : ControllerBase
    {
        private IUnitService unitService;

        public UnitsController(IUnitService unitService)
        {
            this.unitService = unitService;
        }

        // GET: api/Units
        [HttpGet]
        public IActionResult Get()
        {
            List<UnitModel> unitModels = new List<UnitModel>();
            List<Unit> unists = unitService.GetList();

            foreach (var unit in unists)
            {
                UnitModel unitModel = new UnitModel();
                unitModel.Id = unit.Id;
                unitModel.ContractId = unit.ContractId;
                unitModel.Code = unit.Code;
                unitModel.Capacity = unit.Capacity;
                unitModel.BreakdownFee = unit.BreakdownFee;
                unitModel.Brand = unit.Brand;
                unitModel.Address = unit.Address;
                unitModel.CurrentMaintenanceFee = unit.CurrentMaintenanceFee;
                unitModel.MaintenanceStatus = unit.MaintenanceStatus;
                unitModel.OldMaintenanceFee = unit.OldMaintenanceFee;
                unitModel.Name = unit.Name;
                unitModel.Speed = unit.Speed;
                unitModel.Station = unit.Station;
                unitModel.Type = unit.Type;
                unitModel.WarrantyFinishDate = unit.WarrantyFinishDate;

                unitModels.Add(unitModel);
            }

            return Ok(unitModels);
        }

        // GET: api/Units/GetByUnitId/5
        [Route("GetByUnitId/{id}")]
        [HttpGet]
        public IActionResult GetByUnitId(Guid id)
        {
            if (id != Guid.Empty)
            {
                Unit unit = unitService.GetByUnitId(id);
                UnitModel unitModel = new UnitModel {Id = unit.Id ,ContractId = unit.ContractId, Code = unit.Code, Capacity = unit.Capacity, BreakdownFee = unit.BreakdownFee, Brand = unit.Brand, Address = unit.Address, CurrentMaintenanceFee = unit.CurrentMaintenanceFee, MaintenanceStatus = unit.MaintenanceStatus, OldMaintenanceFee = unit.OldMaintenanceFee, Name = unit.Name, Speed = unit.Speed, Station = unit.Station, Type = unit.Type, WarrantyFinishDate = unit.WarrantyFinishDate };

                return Ok(unitModel);
            }

            return BadRequest();
        }

        // GET: api/Units/GetByCustomerContract/5
        [Route("GetByContractId/{contractId}")]
        [HttpGet]
        public IActionResult GetByContractId(Guid contractId)
        {
            List<UnitModel> unitModels = new List<UnitModel>();
            List<Unit> unists = unitService.GetByContractId(contractId);

            foreach (var unit in unists)
            {
                UnitModel unitModel = new UnitModel();
                unitModel.Id = unit.Id;
                unitModel.ContractId = unit.ContractId;
                unitModel.Code = unit.Code;
                unitModel.Capacity = unit.Capacity;
                unitModel.BreakdownFee = unit.BreakdownFee;
                unitModel.Brand = unit.Brand;
                unitModel.Address = unit.Address;
                unitModel.CurrentMaintenanceFee = unit.CurrentMaintenanceFee;
                unitModel.MaintenanceStatus = unit.MaintenanceStatus;
                unitModel.OldMaintenanceFee = unit.OldMaintenanceFee;
                unitModel.Name = unit.Name;
                unitModel.Speed = unit.Speed;
                unitModel.Station = unit.Station;
                unitModel.Type = unit.Type;
                unitModel.WarrantyFinishDate = unit.WarrantyFinishDate;

                unitModels.Add(unitModel);
            }

            return Ok(unitModels);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                unitService.Delete(id);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // POST: api/Units                    
        [HttpPost]
        public IActionResult Post([FromBody] UnitModel unitModel)
        {

            if (ModelState.IsValid)
            {

                Unit unit = new Unit { ContractId = unitModel.ContractId, Code = unitModel.Code, Capacity = unitModel.Capacity, BreakdownFee = unitModel.BreakdownFee, Brand = unitModel.Brand, Address = unitModel.Address, CurrentMaintenanceFee = unitModel.CurrentMaintenanceFee, MaintenanceStatus = unitModel.MaintenanceStatus, OldMaintenanceFee = unitModel.OldMaintenanceFee, Name = unitModel.Name, Speed = unitModel.Speed, Station = unitModel.Station, Type = unitModel.Type, WarrantyFinishDate = unitModel.WarrantyFinishDate };
                unitService.Add(unit);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/Units/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UnitModel unitModel)
        {
            if (ModelState.IsValid)
            {

                Unit unit = unitService.GetByUnitId(unitModel.Id);
                unit.Id = unit.Id;
                unit.ContractId = unitModel.ContractId;
                unit.Code = unitModel.Code;
                unit.Capacity = unitModel.Capacity;
                unit.BreakdownFee = unitModel.BreakdownFee;
                unit.Brand = unitModel.Brand;
                unit.Address = unitModel.Address;
                unit.CurrentMaintenanceFee = unitModel.CurrentMaintenanceFee;
                unit.MaintenanceStatus = unitModel.MaintenanceStatus;
                unit.OldMaintenanceFee = unitModel.OldMaintenanceFee;
                unit.Name = unitModel.Name;
                unit.Speed = unitModel.Speed;
                unit.Station = unitModel.Station;
                unit.Type = unitModel.Type;
                unit.WarrantyFinishDate = unitModel.WarrantyFinishDate;
                unitService.Update(unit);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}