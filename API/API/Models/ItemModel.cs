using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ItemModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        [MinLength(3)]
        public string Desc { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }
    }
}
