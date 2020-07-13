using System.Linq;
using API.Model;
using AutoMapper;

namespace API.Features._StockManagemnt
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap <StockManagemnt, StockManagemntDto>();
        }
    }
}
