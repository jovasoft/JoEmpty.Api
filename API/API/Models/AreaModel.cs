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
    }
}
