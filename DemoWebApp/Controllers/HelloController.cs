using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApp.Controllers
{
    [Route("Hello")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        static List<string> ok = new List<string>() { "Hi" };

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return ok.ToArray();
        }

        [HttpGet("{name}")]
        public string Get(string name)
        {;
            return "Hi " + name;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            ok.Add(id.ToString());
            ok.Add(value);
        }
    }
}
