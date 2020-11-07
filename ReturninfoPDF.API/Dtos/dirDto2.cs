
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReturninfoPDF.API.Dtos
{
    public class dirDto2
    {
        public string nome { get; set; }
        public string email { get; set; }
        public string numero { get; set; }

        public IFormFile File { get; set; }
    }
}
