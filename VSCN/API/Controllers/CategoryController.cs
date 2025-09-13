using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : Controller
    {
        public readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();

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
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Add([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.AddAsync(category);
            if (!result)
                return BadRequest("Thêm mới thất bại");

            return Ok("Thêm mới thành công");
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryDTO category)
        {
           

            var result = await _categoryService.UpdateAsync(category);
            if (!result)
                return BadRequest("Cập nhật thất bại");

            return Ok("Cập nhật thành công");
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _categoryService.RemoveAsync(id);
            if (!result)
                return NotFound("Không tìm thấy category để xóa");

            return Ok("Xóa thành công");
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagedRequestDTO request)
        {
            var result = await _categoryService.GetPaeged(request);
            return Ok(result);
        }

    }
}
