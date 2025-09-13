using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
       

        [Required]
        [MaxLength(250)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; } // Số lượng trong kho

        public string? ImageUrl { get; set; }

        // --- Mối quan hệ với Category ---
        [Required]
        public string? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public virtual Category? Category { get; set; }

        // --- Mối quan hệ với OrderDetail ---
        // Một sản phẩm có thể nằm trong nhiều chi tiết đơn hàng
        [JsonIgnore]
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}