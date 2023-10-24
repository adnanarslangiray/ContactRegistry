using ContantRegistry.Application.Features.Commands.ContactCreate;
using ContantRegistry.Application.Features.Commands.ContactDelete;
using ContantRegistry.Application.Features.Commands.ContactUpdate;
using ContantRegistry.Application.Features.Queries.GetContactById;
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
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("contacts/{id}")]
        public async Task<IActionResult> GetContactById([FromRoute] GetContactByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("contacts")]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("contacts")]
        public async Task<IActionResult> UpdateContact([FromBody] ContactUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("contacts/{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] ContactDeleteCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}