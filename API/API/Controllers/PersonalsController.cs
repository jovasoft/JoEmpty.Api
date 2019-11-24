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
        private IPersonalService personelService;
        public PersonalsController(IPersonalService personelService)
        {
            this.personelService = personelService;
        }

        // GET: api/Personals
        [HttpGet]
        public IActionResult Get()
        {
            List<PersonalModel> personelModels = new List<PersonalModel>();
            List<Personal> personels = personelService.GetList();

            foreach (var personel in personels)
            {
                PersonalModel personelModel = new PersonalModel();
                personelModel.Id = personel.Id;


                personelModels.Add(personelModel);
            }

            return Ok(personelModels);
        }

        // POST: api/Personals                    
        [HttpPost]
        public IActionResult Post([FromBody] PersonalModel personelModel)
        {

            if (ModelState.IsValid)
            {

                Personal personal = new Personal { Id = personelModel.Id};
                personelService.Add(personal);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // DELETE: api/Personals/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                personelService.Delete(id);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}