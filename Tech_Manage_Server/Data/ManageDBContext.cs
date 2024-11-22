using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Data
{
    public class ManageDBContext: IdentityDbContext<ApplicationUser, Role, int>
    {
        public ManageDBContext(DbContextOptions options) : base(options)
        {
            
        }

        #region Dbset
        // DbSets cho các bảng khác
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<RepairItem> RepairItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Feedbacks> Feedbacks { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ giữa ApplicationUser và Customer
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.ApplicationUser)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId);

            // Cấu hình quan hệ giữa ApplicationUser và Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ApplicationUser)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId);

            // Thiết lập ràng buộc unique trên thuộc tính PhoneNumber để đảm bảo số điện thoại là duy nhất trong bảng Customer.
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();

            // Cấu hình quan hệ giữa Order và OrderItem
            modelBuilder.Entity<RepairItem>()
                .HasOne(ri => ri.Repair)
                .WithMany(o => o.RepairItems)
                .HasForeignKey(oi => oi.RepairId);

            modelBuilder.Entity<RepairItem>()
                .HasOne(oi => oi.Inventory)
                .WithMany(ii => ii.RepairItems)
                .HasForeignKey(oi => oi.InventoryId);

            // Cấu hình quan hệ giữa Order và Invoice
            modelBuilder.Entity<Repair>()
                .HasOne(o => o.Invoice)
                .WithOne(i => i.Repair)
                .HasForeignKey<Invoice>(i => i.RepairId);

            // Cấu hình quan hệ giữa Message và ApplicationUser
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ giữa Feedback và Order
            modelBuilder.Entity<Feedbacks>()
                .HasOne(f => f.Repair)
                .WithMany(o => o.Feedbacks)
                .HasForeignKey(f => f.RepairId);

            // Cấu hình quan hệ giữa Notification và ApplicationUser
            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.ApplicationUser)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ giữa AuditLog và ApplicationUser
            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.ApplicationUser)
                .WithMany()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Đổi tên bảng Identity (tuỳ chọn)
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            // Ràng buộc unique cho Email
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Ràng buộc unique cho PhoneNumber
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                    new Role { Id = 2, Name = "Employee", NormalizedName = "EMPLOYEE" },
                    new Role { Id = 3, Name = "Member", NormalizedName = "MEMBER" }
                );
        }
    }
}
