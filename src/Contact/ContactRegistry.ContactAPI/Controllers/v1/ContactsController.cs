using ContantRegistry.Application.Features.Queries.GetContacts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactRegistry.ContactAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet("contacts")]
        public async Task<IActionResult> GetContacts([FromQuery] GetContactsQueryRequest request)
        {
            GetContactsQueryResponse response = await _mediator.Send(request);
            return Ok(response);
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