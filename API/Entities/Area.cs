using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Area : IEntity
    {
        public Guid Id { get; set; }
        public Guid PersonalId { get; set; }
        public Guid UnitId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
