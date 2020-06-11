using ComplaintNet.WebApi.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
