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
    public class AreasController : ResponseController
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
            List<Area> areas = areaService.GetList();

            if (areas == null || areas.Count == 0) return Errors("Bölge listesi bulunamadı.", null, 404);

            List<AreaModel> areaModels = new List<AreaModel>();

            areas.ForEach(x => { areaModels.Add(AreaModel.DtoToModel(x)); });

            return Success(null, areaModels, 200);
        }

        // GET: api/Areas/GetOne/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı bölge yok.",null,404);

            Area area = areaService.Get(id);

            if (area == null) return Errors("Böyle bir ID numaralı bölge yok.",null,404);

            return Success(null, AreaModel.DtoToModel(area));
        }

        // POST: api/Areas                    
        [HttpPost]
        public IActionResult Post([FromBody] AreaModel areaModel)
        {
            if (areaModel.PersonalId == Guid.Empty) return Errors("Personel ID boş olamaz.", null, 404);

            if (!string.IsNullOrEmpty(areaModel.Code))
            {
                Area isExists = areaService.Get(areaModel.Code);

                if (isExists != null) return Errors("Bu kodlu bölge vardır.", null);
            }

            Area area = AreaModel.ModelToDto(areaModel);
            areaService.Add(area);

            if (areaService.Get(area.Id) == null) return Errors("Bölge kaydı yapılamadı.",null,404);

            return Success(null, AreaModel.DtoToModel(area), 201);
        }

        // PUT: api/Areas/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id,[FromBody] AreaModel areaModel)
        {
            if (id == Guid.Empty) return Errors("Bölge ID boş olamaz.", null, 404);

            Area area = areaService.Get(id);

            if (area == null) return Errors("Güncellenmek istenen kayıt yoktur.", null, 404);

            if (Guid.Empty == areaModel.PersonalId) area.PersonalId = areaModel.PersonalId;
            if (!string.IsNullOrEmpty(areaModel.Code))
            {
                Area isExists = areaService.Get(areaModel.Code);

                if (isExists != null && id != isExists.Id) return Errors("Güncellenmek istenen bölge kodu başka bölgeye atanmıştır.", null);

                area.Code = areaModel.Code;
            }
            if (!string.IsNullOrEmpty(areaModel.Name)) area.Name = areaModel.Name;
            if (!string.IsNullOrEmpty(areaModel.Name)) area.Description = areaModel.Description;

            areaService.Update(area);

            return Success(null, AreaModel.DtoToModel(area), 202);

        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı bölge yok.", null, 404);

            Area area = areaService.Get(id);

            if (area == null) return Errors("Böyle bir ID numaralı bölge yok.", null, 404);

            areaService.Delete(id);

            return Success(null, null, 204);
        }
    }
}