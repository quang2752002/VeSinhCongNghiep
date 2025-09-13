using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Share.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User <-> UserDTO
            CreateMap<User, UserDTO>().ReverseMap();

            // Category -> CategoryDTO
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Categories));

            // CategoryDTO -> Category
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // không cho AutoMapper đụng vào Id
                .ForMember(dest => dest.Categories, opt => opt.Ignore()); // tránh map vòng lặp con/cha

            // Article -> ArticleDTO
            CreateMap<Article, ArticleDTO>();

            // ArticleDTO -> Article
            CreateMap<ArticleDTO, Article>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // giữ nguyên Id gốc, tránh null
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // tránh EF load nhầm navigation
        }
    }
}
