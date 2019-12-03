using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UploadFileModel
    {
        public List<IFormFile> Files { get; set; }
    }
}
