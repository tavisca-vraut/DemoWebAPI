using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApp.Controllers
{
    [Route("Hi")]
    [ApiController]
    public class HiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Hello" };
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<string>> Get(string name)
        {
            return new string[] { "Hello " + name };
        }
    }
}
