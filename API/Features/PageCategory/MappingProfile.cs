using API.Model;
using AutoMapper;

namespace API.Features.PageCategory
{
    public class PagesMappingProfile : Profile
    {
        public PagesMappingProfile()
        {                   
            CreateMap<PageItemCategory, PageItemCategoryDto>();            
        }
    }
}