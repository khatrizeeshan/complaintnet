using AutoMapper;
using ComplaintNet.WebApi.Common;
using ComplaintNet.WebApi.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComplaintNet.WebApi.Queries
{
    public class GetComplaintsQuery : IRequest<ComplaintsDto>
    {
    }

    public class GetComplaintsQueryHandler : IRequestHandler<GetComplaintsQuery, ComplaintsDto>
    {
        private readonly ComplaintDbContext _context;
        private readonly ICurrentUser _user;
        private readonly IMapper _mapper;

        public GetComplaintsQueryHandler(ComplaintDbContext context, ICurrentUser user, IMapper mapper)
        {
            _context = context;
            _user = user;
            _mapper = mapper;
        }

        public async Task<ComplaintsDto> Handle(GetComplaintsQuery request, CancellationToken cancellationToken)
        {
            var entities = _context.Complaints.Where(c => c.UserId == _user.Id).ToList();
            var results = _mapper.Map<IList<ComplaintDto>>(entities);
            return new ComplaintsDto() { List = results };
        }
    }
}
