using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CategoryDTO
    {
        public string? Id { get; set; }
        public string? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Summary { get; set; }
        public string? Avatar { get; set; }
        public string? State { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public List<CategoryDTO>? Children { get; set; }

    }
}
