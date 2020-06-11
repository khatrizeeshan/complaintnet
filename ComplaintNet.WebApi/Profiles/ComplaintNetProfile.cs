using AutoMapper;
using ComplaintNet.WebApi.Domain;
using ComplaintNet.WebApi.Queries;

namespace ComplaintNet.WebApi.Profiles
{
    public class ComplaintNetProfile : Profile
    {
        public ComplaintNetProfile()
        {
            CreateMap<ComplaintDto, Complaint>();
            CreateMap<Complaint, ComplaintDto>();
        }
    }
}
