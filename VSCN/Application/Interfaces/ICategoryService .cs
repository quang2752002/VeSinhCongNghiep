using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();

        Task<bool> AddAsync(CategoryDTO category);
        Task<CategoryDTO> GetByIdAsync(string Id);

        Task<bool> UpdateAsync(CategoryDTO category);
        Task<bool> RemoveAsync(string Id );
        Task<List<CategoryDTO>> GetPaeged(PagedRequestDTO pagedRequestDTO);
    }
}
