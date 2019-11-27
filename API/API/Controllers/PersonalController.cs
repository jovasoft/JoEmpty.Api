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
    public class PersonalController : ResponseController
    {
        private IPersonalService personalService;
        public PersonalController(IPersonalService personalService)
        {
            this.personalService = personalService;
        }

        // GET: api/Personal
        [HttpGet]
        public IActionResult Get()
        {
            List<Personal> personals = personalService.GetList();

            if (personals == null || personals.Count == 0) return Errors("Personel listesi bulunamadı.",null,404);

            List<PersonalModel> personalModels = new List<PersonalModel>();

            personals.ForEach(x => { personalModels.Add(PersonalModel.DtoToModel(x)); });

            return Success(null,personalModels,200);

            //return Ok(personalModels);
        }

        // GET: api/Personal/GetOne/id
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı personel yok.");

            Personal personal = personalService.Get(id);

            if (personal == null) return Errors("Böyle bir ID numaralı personel yok.");

            return Success(null,PersonalModel.DtoToModel(personal));
        }

        // POST: api/Personal
        [HttpPost]
        public IActionResult Post([FromBody] PersonalModel personalModel)
        {

            Personal personal = PersonalModel.ModelToDto(personalModel);
            personalService.Add(personal);

            if (personalService.Get(personal.Id) == null) return Errors("Personel kaydı yapılamadı.", null, 404);

            return Success(null, PersonalModel.DtoToModel(personal), 201);
        }

        // PUT: api/Personal/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PersonalModel personalModel)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı personel yok.",null,404);

            Personal personal = personalService.Get(id);

            if (personal == null) return Errors("Böyle bir ID numaralı personel yok.", null, 404);

            if (!string.IsNullOrEmpty(personalModel.FirstName)) personal.FirstName = personalModel.FirstName;
            if (!string.IsNullOrEmpty(personalModel.LastName)) personal.LastName = personalModel.LastName;

            personalService.Update(personal);

            PersonalModel accepted = PersonalModel.DtoToModel(personal);

            return Success(null,accepted,202);
        }

        // DELETE: api/Personal/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı personel yok.", null, 404);

            Personal personal = personalService.Get(id);

            if (personal == null) return Errors("Böyle bir ID numaralı personel yok.", null, 404);

            personalService.Delete(id);

            return Success(null,null,204);
        }
    }
}