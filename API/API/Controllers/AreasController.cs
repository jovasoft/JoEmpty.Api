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
    public class AreasController : ControllerBase
    {
        IAreaService areaService;

        public AreasController(IAreaService areaService)
        {
            this.areaService = areaService;
        }

        // GET: api/Areas
        [HttpGet]
        public IActionResult Get()
        {
            List<AreaModel> areaModels = new List<AreaModel>();
            List<Area> areas = areaService.GetList();

            foreach (var area in areas)
            {
                AreaModel areaModel = new AreaModel();
                areaModel.Id = area.Id;
                areaModel.PersonalId = area.PersonalId;
                areaModel.FacilityId = area.FacilityId;
                areaModel.Code = area.Code;
                areaModel.Name = area.Name;
                areaModel.Description = area.Description;

                areaModels.Add(areaModel);
            }

            return Ok(areaModels);
        }

        // POST: api/Areas                    
        [HttpPost]
        public IActionResult Post([FromBody] AreaModel areaModel)
        {

            if (ModelState.IsValid)
            {

                Area area = new Area { PersonalId = areaModel.PersonalId, FacilityId = areaModel.FacilityId, Code = areaModel.Code, Name = areaModel.Name, Description = areaModel.Description };
                areaService.Add(area);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/Areas/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] AreaModel areaModel)
        {
            if (ModelState.IsValid)
            {

                Area area = areaService.Get(areaModel.Id);
                area.Id = area.Id;
                area.PersonalId = areaModel.PersonalId;
                area.FacilityId = areaModel.FacilityId;
                area.Code = areaModel.Code;
                area.Name = areaModel.Name;
                area.Description = areaModel.Description;
                areaService.Update(area);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                areaService.Delete(id);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}