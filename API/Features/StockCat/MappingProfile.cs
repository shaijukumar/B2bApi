using System.Linq;
using API.Model;
using AutoMapper;

namespace API.Features._StockCat
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap <StockCat, StockCatDto>();
        }
    }
}
