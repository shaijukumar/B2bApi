using System.Linq;
using API.Model;
using AutoMapper;


namespace Application.Pages
{
    public class PagesMappingProfile : Profile
    {
        public PagesMappingProfile()
        {
            
            CreateMap<Page, PageDto>()
                 .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                 .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.Category.Id));
        }
    }
}