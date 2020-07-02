using System.Linq;
using API.Model;
using AutoMapper;

namespace API.Features._OrderMaster
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap <OrderMaster, OrderMasterDto>();
        }
    }
}
