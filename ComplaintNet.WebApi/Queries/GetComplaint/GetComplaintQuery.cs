using AutoMapper;
using ComplaintNet.WebApi.Common;
using ComplaintNet.WebApi.Exceptions;
using ComplaintNet.WebApi.Persistance;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Queries
{
    public class GetComplaintQuery : IRequest<ComplaintDto>
    {
        public int Id { get; set; }
    }

    public class GetComplaintQueryHandler : IRequestHandler<GetComplaintQuery, ComplaintDto>
    {
        private readonly ComplaintDbContext _context;
        private readonly ICurrentUser _user;
        private readonly IMapper _mapper;

        public GetComplaintQueryHandler(ComplaintDbContext context, ICurrentUser user, IMapper mapper)
        {
            _context = context;
            _user = user;
            _mapper = mapper;
        }

        public async Task<ComplaintDto> Handle(GetComplaintQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Complaints.FindAsync(request.Id);

            if (entity == null || entity.UserId != _user.Id)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<ComplaintDto>(entity);
        }
    }
}
