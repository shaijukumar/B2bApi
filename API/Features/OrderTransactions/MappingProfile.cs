using System.Linq;
using API.Model;
using AutoMapper;

namespace API.Features._OrderTransactions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap <OrderTransactions, OrderTransactionsDto>();
        }
    }
}
