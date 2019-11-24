using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CustomerContactModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string InternalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
    }
}
