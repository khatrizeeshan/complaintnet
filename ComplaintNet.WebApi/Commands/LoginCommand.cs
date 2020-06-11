using ComplaintNet.WebApi.Domain;
using ComplaintNet.WebApi.Exceptions;
using ComplaintNet.WebApi.Persistance;
using ComplaintNet.WebApi.Security;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Commands
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly ComplaintDbContext _context;
        private readonly ITokenGenerator _generator;

        public LoginCommandHandler(ComplaintDbContext context, ITokenGenerator generator)
        {
            _context = context;
            _generator = generator;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.SingleOrDefaultAsync(u => u.IsActive && u.Email == request.Email && u.Password == request.Password);

            if (entity == null)
            {
                throw new NotFoundException(); //should have a custom message here to send proper http status to client
            }

            var result = new LoginResponseDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Token = _generator.Generate(entity)
            };

            return result;
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.Email).MaximumLength(200).NotEmpty();
            RuleFor(v => v.Password).MaximumLength(100).NotEmpty();
        }
    }

    public class LoginResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }
    }
}
