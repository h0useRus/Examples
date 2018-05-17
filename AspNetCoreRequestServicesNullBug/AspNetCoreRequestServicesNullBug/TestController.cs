using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRequestServicesNullBug
{
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("simple")]
        public IActionResult SimpleOk()
        {
            return Ok();
        }
        [HttpGet("valued")]
        public IActionResult ValuedOk()
        {
            return Ok(42);
        }
    }
}