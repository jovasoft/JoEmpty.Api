﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserModel
    {
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [MinLength(3)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(20)]
        [MinLength(3)]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(32)]
        [MinLength(6)]
        [RegularExpression("^((?=.*?[a-z])(?=.*?[0-9])).{6,32}$")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [MinLength(3)]
        public string Mail { get; set; }
    }
}
