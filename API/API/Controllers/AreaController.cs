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
    public class AreaController : ResponseController
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

            if (areas == null || areas.Count == 0) return Error("Eşleşen kayıt bulunamadı.", 404);

            List<AreaModel> areaModels = new List<AreaModel>();

            areas.ForEach(x => { areaModels.Add(AreaModel.DtoToModel(x)); });

            return Success(areaModels);
        }

        // GET: api/Area/GetOne/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Error("Bölge bulunamadı.", 404);

            Area area = areaService.Get(id);

            if (area == null) return Error("Bölge bulunamadı.", 404);

            return Success(AreaModel.DtoToModel(area));
        }

        // POST: api/Area                   
        [HttpPost]
        public IActionResult Post([FromBody] AreaModel areaModel)
        {
            if (areaModel.PersonalId == Guid.Empty) return Error("Personel bulunamadı.", 404);

            if (!string.IsNullOrEmpty(areaModel.Code))
            {
                Area isExists = areaService.Get(areaModel.Code);

                if (isExists != null) return Error("Bu koda ait bir bölge zaten var.");
            }

            Area area = AreaModel.ModelToDto(areaModel);
            areaService.Add(area);

            if (areaService.Get(area.Id) == null) return Error("Bölge eklenemedi.");

            return Success(AreaModel.DtoToModel(area), 201);
        }

        // PUT: api/Area/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id,[FromBody] AreaModel areaModel)
        {
            if (id == Guid.Empty) return Error("Bölge bulunamadı.", 404);

            Area area = areaService.Get(id);

            if (area == null) return Error("Bölge bulunamadı.", 404);

            if (Guid.Empty == areaModel.PersonalId) area.PersonalId = areaModel.PersonalId;
            if (!string.IsNullOrEmpty(areaModel.Code))
            {
                Area isExists = areaService.Get(areaModel.Code);

                if (isExists != null && id != isExists.Id) return Error("Güncellenmek istenen bölge kodu başka bir bölgeye ait.");

                area.Code = areaModel.Code;
            }
            if (!string.IsNullOrEmpty(areaModel.Name)) area.Name = areaModel.Name;
            if (!string.IsNullOrEmpty(areaModel.Name)) area.Description = areaModel.Description;

            areaService.Update(area);

            return Success(AreaModel.DtoToModel(area), 202);

        }

        // DELETE: api/Area/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Error("Bölge bulunamadı.", 404);

            Area area = areaService.Get(id);

            if (area == null) return Error("Bölge bulunamadı.", 404);

            areaService.Delete(id);

            return Success(null, 204);
        }
    }
}