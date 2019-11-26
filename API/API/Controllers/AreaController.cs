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
    public class AreaController : ControllerBase
    {
        IAreaService areaService;

        public AreaController(IAreaService areaService)
        {
            this.areaService = areaService;
        }

        // GET: api/Areas
        [HttpGet]
        public IActionResult Get()
        { 
            List<Area> areas = areaService.GetList();

            if (areas == null || areas.Count == 0) return NotFound();
       
            List<AreaModel> areaModels = new List<AreaModel>();

            areas.ForEach(x => { areaModels.Add(AreaModel.DtoToModel(x)); });

            return Ok(areaModels);
        }

        // GET: api/Areas/GetOne/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Area area = areaService.Get(id);

            if (area == null) return NotFound();

            return Ok(AreaModel.DtoToModel(area));
        }

        // POST: api/Areas                    
        [HttpPost]
        public IActionResult Post([FromBody] AreaModel areaModel)
        {
            if (areaModel.PersonalId == Guid.Empty) return NotFound();

            if (!string.IsNullOrEmpty(areaModel.Code))
            {
                Area isExists = areaService.Get(areaModel.Code);

                if (isExists != null) return BadRequest();
            }

            Area area = AreaModel.ModelToDto(areaModel);
            areaService.Add(area);

            AreaModel created = AreaModel.DtoToModel(area);

            return CreatedAtAction(nameof(GetOne), new { created.Id }, created);
        }

        // PUT: api/Areas/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id,[FromBody] AreaModel areaModel)
        {
            if (id == Guid.Empty) return NotFound();

            Area area = areaService.Get(id);

            if (area == null) return NotFound();
            
            if (Guid.Empty == areaModel.PersonalId) area.PersonalId = areaModel.PersonalId;
            if (!string.IsNullOrEmpty(areaModel.Code))
            {
                Area isExists = areaService.Get(areaModel.Code);

                if (isExists != null && id != isExists.Id) return BadRequest();

                area.Code = areaModel.Code;
            }
            if (!string.IsNullOrEmpty(areaModel.Name)) area.Name = areaModel.Name;
            if (!string.IsNullOrEmpty(areaModel.Name)) area.Description = areaModel.Description;

            areaService.Update(area);

            AreaModel accepted = AreaModel.DtoToModel(area);

            return Accepted(accepted);
            
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Area area = areaService.Get(id);

            if (area == null) return NotFound();

            areaService.Delete(id);

            return NoContent();
        }
    }
}