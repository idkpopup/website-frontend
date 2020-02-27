using Microsoft.AspNetCore.Mvc;
using Site.ServiceModels;
using Site.Services;

namespace Site.Controllers
{
    [Route("api/register")]
    public class RegisterController : Controller
    {
        /// <summary>
        /// Leverages S3 registration service to regiser a contact.
        /// </summary>
        /// <param name="contact">A contact taken from the registration form.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post([FromBody] Contact contact)
        {
            IRegisterContactService registrationService = new RegisterS3ContactService();
            var response  = registrationService.Register(contact);
            return new JsonResult(response);
        }
    }
}