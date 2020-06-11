using ComplaintNet.WebApi.Common;
using ComplaintNet.WebApi.Domain;
using ComplaintNet.WebApi.Persistance;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Commands
{
    public class CreateComplaintCommand : IRequest<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

    }

    public class CreateComplaintCommandHandler : IRequestHandler<CreateComplaintCommand, int>
    {
        private readonly ComplaintDbContext _context;
        private readonly ICurrentUser _user;

        public CreateComplaintCommandHandler(ComplaintDbContext context, ICurrentUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<int> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
        {
            var entity = new Complaint
            {
                Title = request.Title,
                Description = request.Description,
                Date = DateTime.UtcNow,
                UserId = _user.Id
            };

            _context.Complaints.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

    public class CreateComplaintCommandValidator : AbstractValidator<CreateComplaintCommand>
    {
        public CreateComplaintCommandValidator()
        {
            RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
            RuleFor(v => v.Description).MaximumLength(1000).NotEmpty();
        }
    }
}
