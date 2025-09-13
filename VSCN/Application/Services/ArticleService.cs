using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IBaseRepository<Article> _baseRepository;
        private readonly IMapper _mapper;

        public ArticleService(IBaseRepository<Article> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ArticleDTO articleDTO)
        {
            var entity = _mapper.Map<Article>(articleDTO);
            
            var result = await _baseRepository.AddAsync(entity);
            return result != null;
        }

        public async Task<List<ArticleDTO>> GetAllAsync()
        {
            var entities = await _baseRepository.GetAllAsync();
           // entities = entities.Where(x => x.ParentId == null);
            return _mapper.Map<List<ArticleDTO>>(entities);
        }

        public async Task<ArticleDTO> GetByIdAsync(string Id)
        {
            var entity = await _baseRepository.GetByIdAsync(Id);
            return _mapper.Map<ArticleDTO>(entity);
        }

        public async Task<List<ArticleDTO>> GetPaeged(PagedRequestDTO pagedRequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAsync(string Id)
        {
            var entity = await _baseRepository.GetByIdAsync(Id);
            if (entity == null)
            {
                return false;
            }

            var result = await _baseRepository.RemoveAsync(entity);
            return result != null;
        }

        public async Task<bool> UpdateAsync(ArticleDTO articleDTO)
        {
            var entity = await _baseRepository.GetByIdAsync(articleDTO.Id);
            if (entity == null)
                return false; // Không tồn tại -> không update được

            _mapper.Map(articleDTO, entity);

            var result = await _baseRepository.UpdateAsync(entity);
            return result != null;
        }

    }
}
