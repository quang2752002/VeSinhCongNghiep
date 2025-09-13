using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Share.Constant; // Cần cho Data Seeding

namespace Infrastructure
{
   
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Khai báo các DbSet cho các entity tùy chỉnh của bạn
        // EF Core sẽ tự động tạo các bảng tương ứng từ các DbSet này.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<WebsiteConfig> WebsiteConfigs { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Rất quan trọng: Luôn gọi phương thức base.OnModelCreating() đầu tiên
            // khi kế thừa từ IdentityDbContext. Nó sẽ cấu hình tất cả các bảng Identity.
            base.OnModelCreating(modelBuilder);

            // Cấu hình Fluent API (tùy chọn nhưng khuyến khích để rõ ràng và kiểm soát tốt hơn)

            // 1. Cấu hình kiểu dữ liệu cho các cột tiền tệ để đảm bảo độ chính xác
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18, 2)");

            // 2. Data Seeding: Thêm dữ liệu mẫu cho bảng Categories khi database được tạo lần đầu
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = "1", Name = "Laptop"  },
                new Category { Id = "2", Name = "PC - Máy tính để bàn" },
                new Category { Id = "3", Name = "Linh kiện" },
                new Category { Id = "4", Name = "Phụ kiện" }
            );

            // Bạn cũng có thể thêm các cấu hình cho mối quan hệ ở đây nếu cần,
            // nhưng EF Core thường có thể tự suy ra chúng từ các lớp Entity.
        }
    }
}