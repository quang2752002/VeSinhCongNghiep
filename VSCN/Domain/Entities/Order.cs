using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    // Dùng Enum để quản lý trạng thái đơn hàng một cách an toàn và rõ ràng
    public enum OrderStatus
    {
        Pending,      // Đang chờ xử lý
        Processing,   // Đang xử lý
        Shipped,      // Đã giao hàng
        Completed,    // Hoàn thành
        Cancelled     // Đã hủy
    }

    public class Order:BaseEntity
    {
      

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        // --- Mối quan hệ với User (Customer) ---
        [Required]
        public string? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual User? Customer { get; set; }

        // --- Mối quan hệ với OrderDetail ---
        // Một đơn hàng có nhiều chi tiết đơn hàng
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}