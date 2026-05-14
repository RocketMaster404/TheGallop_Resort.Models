using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.Services;

namespace TheGallop_Resort.Api.Controllers
{
    public abstract class BaseController : Controller
    {

        [NonAction]
        protected ActionResult ToErrorResponse(ServiceResult result)
        {
            return result.Status switch
            {
                ServiceResultStatus.NotFound => NotFound(result.ErrorMessage),
                ServiceResultStatus.ValidationError => BadRequest(result.ErrorMessage),
                _ => BadRequest(result.ErrorMessage)
            };
        }
    }
}
