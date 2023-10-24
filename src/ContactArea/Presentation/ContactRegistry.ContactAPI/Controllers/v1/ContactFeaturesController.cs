using ContantRegistry.Application.Features.Commands.ContactFeatureCreate;
using ContantRegistry.Application.Features.Commands.ContactFeatureDelete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactRegistry.ContactAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ContactFeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactFeaturesController(IMediator mediator)
        {
            _mediator=mediator;
        }

        // create
        [HttpPost]
        [Route("contact-features")]
        public async Task<IActionResult> CreateContactFeature([FromBody] ContactFeatureCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        // delete
        [HttpDelete]
        [Route("contact-features/{id}")]
        public async Task<IActionResult> RemoveContactFeature([FromRoute] ContactFeatureDeleteCommandRequest request )
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}