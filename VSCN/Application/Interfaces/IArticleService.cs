using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IArticleService
    {
        Task<List<ArticleDTO>> GetAllAsync();

        Task<bool> AddAsync(ArticleDTO articleDTO);
        Task<ArticleDTO> GetByIdAsync(string Id);

        Task<bool> UpdateAsync(ArticleDTO articleDTO);
        Task<bool> RemoveAsync(string Id);
        Task<List<ArticleDTO>> GetPaeged(PagedRequestDTO pagedRequestDTO);
    }
}
