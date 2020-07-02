using System.Linq;
using API.Model;
using AutoMapper;

namespace API.Features.Catlog
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Catalog, CatlogDto>()
                .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.DisplayName))
                .ForMember(d => d.SupplierId, o => o.MapFrom(s => s.Supplier.Id))
                .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.Category.Id))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))                
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<Catalog, CatlogListDto>()  
                //.ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault().Url));
        }
    }
} 