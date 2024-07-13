using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Onlinehelpdesk.Models;

namespace Onlinehelpdesk.Data
{
    public class Onlinehelpdeskdb: IdentityDbContext
    {
        public Onlinehelpdeskdb(DbContextOptions<Onlinehelpdeskdb> options) : base(options)
        {

        }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Category> Category{ get; set; }
        public virtual DbSet<Discussion> Discussion { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Photo> Photo{ get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Status> Status { get; set; }
       
        public virtual DbSet<Ticket> Ticket { get; set; }
       

        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode(false);
                entity.Property(e => e.UserName)
               .HasMaxLength(50)
               .IsRequired()
               .IsUnicode(false);
                entity.Property(e => e.FullName)
               .HasMaxLength(60)
               .IsUnicode(false);
                entity.Property(e => e.Password)
               .HasMaxLength(200)
               .IsUnicode(false);
                entity.Property(e => e.Email)
               .HasMaxLength(200)
               .IsUnicode(true);
                entity.HasOne(d => d.Role)
                .WithMany(p => p.Account)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Role");
                base.OnModelCreating(modelBuilder);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasMaxLength(200)
                      .IsUnicode(false);

                entity.HasData(
                    new Role { Id = 1, Name = "Administrator" },
                    new Role { Id = 2, Name = "Support" },
                    new Role { Id = 3, Name = "Employee" }
                );
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            });
            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
                //entity.Property(e => e.Status1).HasColumnName("Status");
                modelBuilder.Entity<Status>()
           .Property(s => s.Status1)
           .HasColumnName("Status");

            });
            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");
                entity.Property(e => e.CreateDate).HasColumnType("date");
                 entity.HasOne(d => d.Ticket)
            .WithMany(t => t.Discussions)
            .HasForeignKey(d => d.TicketId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("date");
                entity.Property(e => e.Title)
              
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
                entity.HasOne(d => d.Category)
                .WithMany(p => p.Ticket)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Category");
               

            });


        }
    }
}
