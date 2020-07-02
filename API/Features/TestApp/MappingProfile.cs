using System.Linq;
using API.Model;
using AutoMapper;

namespace API.Features._TestApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap <TestApp, TestAppDto>();
        }
    }
}
