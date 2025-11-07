using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Model.Request;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;
        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContact([FromBody] ContactRequest contact)
        {
            if (contact == null)
                return BadRequest("Invalid contact data.");

            var result = await _service.SubmitContactAsync(contact);
            if (!result)
                return BadRequest("Please provide all required fields.");

            return Ok(new
            {
                message = "Your message has been submitted successfully! We will contact you soon."
            });
        }

    }
}
