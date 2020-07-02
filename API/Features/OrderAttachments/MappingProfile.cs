using System.Linq;
using API.Model;
using AutoMapper;

namespace API.Features._OrderAttachments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap <OrderAttachments, OrderAttachmentsDto>();
        }
    }
}
