using Microsoft.AspNetCore.Mvc;

namespace ContactRegistry.ContactAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ContactFeaturesController : ControllerBase
    {

        [HttpGet]
        [Route("contacts/{contactId}/features")]
        public IActionResult GetContactFeatures(string contactId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("contacts/{contactId}/features/{featureId}")]
        public IActionResult GetContactFeatureById(string contactId, string featureId)
        {
            return Ok();
        }




    }
}