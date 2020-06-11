using ComplaintNet.WebApi.Common;
using ComplaintNet.WebApi.Exceptions;
using ComplaintNet.WebApi.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Commands
{
    public class DeleteComplaintCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteComplaintCommandHandler : IRequestHandler<DeleteComplaintCommand>
    {
        private readonly ComplaintDbContext _context;
        private readonly ICurrentUser _user;

        public DeleteComplaintCommandHandler(ComplaintDbContext context, ICurrentUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Unit> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Complaints.FindAsync(request.Id);

            if (entity == null || entity.UserId != _user.Id)
            {
                throw new NotFoundException();
            }

            _context.Complaints.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
