﻿using System;
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

            if (personals == null || personals.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<PersonalModel> personalModels = new List<PersonalModel>();

            personals.ForEach(x => { personalModels.Add(PersonalModel.DtoToModel(x)); });

            return Success(personalModels);
        }

        // GET: api/Personal/GetOne/id
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Error("Personel bulunamadı.", 404);

            Personal personal = personalService.Get(id);

            if (personal == null) return Error("Personel bulunamadı.", 404);

            return Success(PersonalModel.DtoToModel(personal));
        }

        // POST: api/Personal
        [HttpPost]
        public IActionResult Post([FromBody] PersonalModel personalModel)
        {
            Personal personal = PersonalModel.ModelToDto(personalModel);
            personalService.Add(personal);

            if (personalService.Get(personal.Id) == null) return Error("Personel eklenemedi.");

            return Success(PersonalModel.DtoToModel(personal), 201);
        }

        // PUT: api/Personal/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PersonalModel personalModel)
        {
            if (id == Guid.Empty) return Error("Personel bulunamadı.", 404);

            Personal personal = personalService.Get(id);

            if (personal == null) return Error("Personel bulunamadı.", 404);

            if (!string.IsNullOrEmpty(personalModel.FirstName)) personal.FirstName = personalModel.FirstName;
            if (!string.IsNullOrEmpty(personalModel.LastName)) personal.LastName = personalModel.LastName;

            personalService.Update(personal);

            return Success(PersonalModel.DtoToModel(personal), 202);
        }

        // DELETE: api/Personal/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Error("Personel bulunamadı.", 404);

            Personal personal = personalService.Get(id);

            if (personal == null) return Error("Personel bulunamadı.", 404);

            personalService.Delete(id);

            return Success(null, 204);
        }
    }
}