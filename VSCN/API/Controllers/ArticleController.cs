using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        public readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _articleService.GetAllAsync();

                if (categories == null || !categories.Any())
                    return NoContent(); // 204 khi không có dữ liệu

                return Ok(categories); // 200 khi có dữ liệu
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _articleService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ArticleDTO articleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _articleService.AddAsync(articleDTO);
            if (!result)
                return BadRequest("Thêm mới thất bại");

            return Ok("Thêm mới thành công");
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ArticleDTO articleDTO)
        {
            if (articleDTO == null || articleDTO.Id==null)
            {
                return BadRequest("Cập nhật thất bại");
            }
            var result = await _articleService.UpdateAsync(articleDTO);
            if (!result)
                return NotFound("Không tìm thấy bài viết để xóa");
            return Ok("Cập nhật thành công");
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _articleService.RemoveAsync(id);
            if (!result)
                return NotFound("Không tìm thấy bài viết để xóa");
            return Ok("Xóa thành công");
        }
    }
}
