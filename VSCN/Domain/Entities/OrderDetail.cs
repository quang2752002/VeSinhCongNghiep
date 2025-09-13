using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderDetail:BaseEntity
    {
      

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } // Lưu lại giá tại thời điểm mua hàng

        // --- Mối quan hệ với Order ---
        [Required]
        public string? OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        // --- Mối quan hệ với Product ---
        [Required]
        public string? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}