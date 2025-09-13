using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category:BaseEntity
    {

        public string? ParentId { get; set; } 
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Summary { get; set; }
        public string? Avatar { get; set; }
        public string? State { get; set; }

        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        public virtual ICollection<Article>? Articles { get; set; }


    }
}
