using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using netMvcTest.Dto.Article;
using netMvcTest.Model.Entity;

namespace netMvcTest.MVC.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.CategoryIds,
                    opt =>
                        opt.MapFrom(src =>
                            src.ArticleToCategories.Select(x => x.BlogCategoryId)))
                .ForMember(dest => dest.UserEmail,
                    opt => opt.MapFrom(src => src.User.Email));
            CreateMap<ArticleDto, Article>();
            CreateMap<Article, AddArticleDto>();
            CreateMap<AddArticleDto, Article>();
            
            CreateMap<Article, EditArticleDto>();
            CreateMap<EditArticleDto, Article>();
        }
    }
}