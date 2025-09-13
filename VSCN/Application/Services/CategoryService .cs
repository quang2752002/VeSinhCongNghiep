using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Share.Constant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _baseRepository;
        private readonly IMapper _mapper;

        public CategoryService(IBaseRepository<Category> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CategoryDTO categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);
            var result = await _baseRepository.AddAsync(entity);
            return result != null;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            var result = await _baseRepository.RemoveAsync(entity);
            return result != null;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var entities = await _baseRepository.GetAllAsync();
            //entities = entities.Where(x => x.State!=State.DELETED);
            return _mapper.Map<List<CategoryDTO>>(entities);
        }

        public async Task<CategoryDTO> GetByIdAsync(string id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task<bool> UpdateAsync(CategoryDTO categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);
            var result = await _baseRepository.UpdateAsync(entity);
            return result != null;
        }

        public Task<List<CategoryDTO>> GetPaeged(PagedRequestDTO pagedRequestDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
