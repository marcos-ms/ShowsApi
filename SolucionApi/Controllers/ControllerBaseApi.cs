using Microsoft.AspNetCore.Mvc;
using SolucionApi.Shared;

namespace SolucionApi.Controllers;

public abstract class ControllerBaseApi : ControllerBase
{
    protected async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (ArgumentNullException)
        {
            return BadRequest(MessagesControllers.BadRequest);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(MessagesControllers.Unauthorized);
        }
        catch (Exception)
        {
            return StatusCode(500, MessagesControllers.Unexpected);
        }
    }
}
