using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AreaModel
    {
        public Guid Id { get; set; }
        public Guid PersonalId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public static AreaModel DtoToModel(Area area)
        {
            return new AreaModel
            {
                Id = area.Id,
                PersonalId = area.PersonalId,
                Code = area.Code,
                Name = area.Name,
                Description = area.Description
            };
        }


        public static Area ModelToDto(AreaModel areaModel)
        {
            return new Area
            {
                Id = Guid.NewGuid(),
                PersonalId = areaModel.PersonalId,
                Code = areaModel.Code,
                Name = areaModel.Name,
                Description = areaModel.Description
            };
        }
    }
}
