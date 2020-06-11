using ComplaintNet.WebApi.Commands;
using ComplaintNet.WebApi.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Controllers
{
    [Authorize]
    public class ComplaintsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<ComplaintsDto>> Get()
        {
            return await Mediator.Send(new GetComplaintsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintDto>> Get(int id)
        {
            return await Mediator.Send(new GetComplaintQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateComplaintCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateComplaintCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteComplaintCommand { Id = id });

            return NoContent();
        }
    }
}
