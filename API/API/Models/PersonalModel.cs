using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PersonalModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public static PersonalModel DtoToModel(Personal personal)
        {
            return new PersonalModel
            {
                FirstName = personal.FirstName,
                Id = personal.Id,
                LastName = personal.LastName
            };
        }

        public static Personal ModelToDto(PersonalModel personalModel)
        {
            return new Personal
            {
                FirstName = personalModel.FirstName,
                Id = personalModel.Id,
                LastName = personalModel.LastName
            };
        }
    }
}
