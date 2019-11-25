using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AreaModel
    {
        public Guid Id { get; set; }
        public Guid PersonalId { get; set; }
        public Guid FacilityId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static AreaModel DtoToModel(Area area)
        {
            return new AreaModel
            {
                Id = area.Id,
                PersonalId = area.PersonalId,
                FacilityId = area.FacilityId,
                Code = area.Code,
                Name = area.Name,
                Description = area.Description
            };
        }


        public static Area ModelToDto(AreaModel areaModel)
        {
            return new Area
            {

                PersonalId = areaModel.PersonalId,
                FacilityId = areaModel.FacilityId,
                Code = areaModel.Code,
                Name = areaModel.Name,
                Description = areaModel.Description
            };
        }
    }
}
