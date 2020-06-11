using ComplaintNet.WebApi.Domain;
using ComplaintNet.WebApi.Persistance;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Commands
{
    public class RegisterCommand : IRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly ComplaintDbContext _context;

        public RegisterCommandHandler(ComplaintDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                IsActive = true,
            };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(v => v.Name).MaximumLength(200).NotEmpty();
            RuleFor(v => v.Email).MaximumLength(200).NotEmpty();
            RuleFor(v => v.Password).MaximumLength(100).NotEmpty();
        }
    }
}
