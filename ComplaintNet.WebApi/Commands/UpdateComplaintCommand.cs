using ComplaintNet.WebApi.Common;
using ComplaintNet.WebApi.Exceptions;
using ComplaintNet.WebApi.Persistance;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Commands
{
    public class UpdateComplaintCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class UpdateComplaintCommandHandler : IRequestHandler<UpdateComplaintCommand>
    {
        private readonly ComplaintDbContext _context;
        private readonly ICurrentUser _user;

        public UpdateComplaintCommandHandler(ComplaintDbContext context, ICurrentUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Unit> Handle(UpdateComplaintCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Complaints.FindAsync(request.Id);

            if (entity == null || entity.UserId != _user.Id)
            {
                throw new NotFoundException();
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.LastUpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateComplaintCommandValidator : AbstractValidator<UpdateComplaintCommand>
    {
        public UpdateComplaintCommandValidator()
        {
            RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
            RuleFor(v => v.Description).MaximumLength(1000).NotEmpty();
        }
    }
}
