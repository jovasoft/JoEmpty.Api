using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ClientModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Note { get; set; }
        public string CurrentCode { get; set; }


        public static ClientModel DtoToModel(Client client)
        {
            return new ClientModel
            {
                Id = client.Id,
                Note = client.Note,
                Title = client.Title,
                Address = client.Address,
                CurrentCode = client.CurrentCode,
                City = client.City,
                District = client.District
            };
        }

        public static Client ModelToDto(ClientModel clientModel)
        {
            return new Client
            {
                Id = Guid.NewGuid(),
                Note = clientModel.Note,
                Title = clientModel.Title,
                Address = clientModel.Address,
                CurrentCode = clientModel.CurrentCode,
                City = clientModel.City,
                District = clientModel.District
            };
        }
    }
}
