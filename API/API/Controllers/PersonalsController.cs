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
    public class PersonalsController : ControllerBase
    {
        private IPersonalService personalService;
        public PersonalsController(IPersonalService personalService)
        {
            this.personalService = personalService;
        }

        // GET: api/Personals
        [HttpGet]
        public IActionResult Get()
        {
            List<Personal> personals = personalService.GetList();

            if (personals == null || personals.Count == 0) return NotFound();

            List<PersonalModel> personalModels = new List<PersonalModel>();

            personals.ForEach(x => { personalModels.Add(PersonalModel.DtoToModel(x)); });

            return Ok(personalModels);
        }

        // GET: api/Personals/GetOne/id
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Personal personal = personalService.Get(id);

            if (personal == null) return NotFound();

            return Ok(PersonalModel.DtoToModel(personal));
        }

        // POST: api/Personals
        [HttpPost]
        public IActionResult Post([FromBody] PersonalModel personalModel)
        {
            //if (personelModel.ClientId == Guid.Empty) return NotFound();

            Personal personal = PersonalModel.ModelToDto(personalModel);
            personalService.Add(personal);

            PersonalModel created = PersonalModel.DtoToModel(personal);

            return CreatedAtAction(nameof(GetOne), new { created.Id }, created);
        }

        // PUT: api/Personals/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PersonalModel personalModel)
        {
            if (id == Guid.Empty) return NotFound();

            Personal personal = personalService.Get(id);

            if (personal == null) return NotFound();

            if (!string.IsNullOrEmpty(personalModel.FirstName)) personal.FirstName = personalModel.FirstName;
            if (!string.IsNullOrEmpty(personalModel.LastName)) personal.LastName = personalModel.LastName;

            personalService.Update(personal);

            PersonalModel accepted = PersonalModel.DtoToModel(personal);

            return Accepted(accepted);
        }

        // DELETE: api/Personals/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Personal personal = personalService.Get(id);

            if (personal == null) return NotFound();

            personalService.Delete(id);

            return NoContent();
        }
    }
}