using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ClientContactModel
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

        public static ClientContactModel DtoToModel(ClientContact clientContact)
        {
            return new ClientContactModel {
                ClientId = clientContact.ClientId,
                Department = clientContact.Department,
                FirstName = clientContact.FirstName,
                Id = clientContact.Id,
                InternalNumber = clientContact.InternalNumber,
                LastName = clientContact.LastName,
                MailAddress = clientContact.MailAddress,
                PhoneNumber = clientContact.PhoneNumber,
                Title = clientContact.Title
            };
        }

        public static ClientContact ModelToDto(ClientContactModel clientContactModel)
        {
            return new ClientContact {
                ClientId = clientContactModel.ClientId,
                Department = clientContactModel.Department,
                FirstName = clientContactModel.FirstName,
                Id = clientContactModel.Id,
                InternalNumber = clientContactModel.InternalNumber,
                LastName = clientContactModel.LastName,
                MailAddress = clientContactModel.MailAddress,
                PhoneNumber = clientContactModel.PhoneNumber,
                Title = clientContactModel.Title
            };
        }

    }
}
