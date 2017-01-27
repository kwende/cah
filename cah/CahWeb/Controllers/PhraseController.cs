using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CahWeb.Controllers
{
    [Route("api/[controller]")]
    public class PhraseController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(basePath, "sayings.txt")); 

            return lines;
        }
    }
}
