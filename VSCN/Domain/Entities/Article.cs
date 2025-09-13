using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Article:BaseEntity
    {
        public string? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? TypeArticle { get; set; }
        public string? Content { get; set; }
        public string? Avatar { get; set; }
        public string? State { get; set; }

        public virtual Category? Category { get; set; }
    }
}
