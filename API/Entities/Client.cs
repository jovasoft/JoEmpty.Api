using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Client : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Note { get; set; }
        public string CurrentCode { get; set; }
    }
}
