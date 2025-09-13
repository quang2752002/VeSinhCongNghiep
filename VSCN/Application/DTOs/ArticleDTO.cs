using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ArticleDTO
    {
        public string? Id { get; set; }
        public string? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? TypeArticle { get; set; }
        public string? Content { get; set; }
        public string? Avatar { get; set; }
        public bool? Trash { get; set; }
        public bool? Active { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
