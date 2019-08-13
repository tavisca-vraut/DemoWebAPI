using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApp.Controllers
{
    [Route("Hello")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Hi" };
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<string>> Get(string name)
        {
            return new string[] { "Hi " + name }; 
        }
    }
}
