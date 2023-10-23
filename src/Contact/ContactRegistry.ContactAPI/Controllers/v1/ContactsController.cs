using Microsoft.AspNetCore.Mvc;

namespace ContactRegistry.ContactAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ContactsController : ControllerBase
    {
        [HttpGet("contacts")]
        public IActionResult GetContacts()
        {
            return Ok();
        }

        [HttpGet("contacts/{id}")]
        public IActionResult GetContactById(string id)
        {
            return Ok();
        }

        [HttpPost("contacts")]
        public IActionResult CreateContact()
        {
            return Ok();
        }

        [HttpPut("contacts/{id}")]
        public IActionResult UpdateContact(string id)
        {
            return Ok();
        }

        [HttpDelete("contacts/{id}")]
        public IActionResult DeleteContact(string id)
        {
            return Ok();
        }
    }
}