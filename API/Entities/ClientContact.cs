using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ClientContact : IEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string InternalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
    }
}
