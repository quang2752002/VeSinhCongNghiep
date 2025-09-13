using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Common
{
    public class ServiceRegister
    {
        public static void Register(IServiceCollection services)
        {

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //// Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IArticleService, ArticleService>();
           
            services.AddScoped<ITokenService, TokenService>();

            //// Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
