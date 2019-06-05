using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(32)]
        [MinLength(6)]
        [RegularExpression("^((?=.*?[a-z])(?=.*?[0-9])).{6,32}$")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(32)]
        [MinLength(6)]
        [RegularExpression("^((?=.*?[a-z])(?=.*?[0-9])).{6,32}$")]
        public string NewPassword { get; set; }
    }
}
