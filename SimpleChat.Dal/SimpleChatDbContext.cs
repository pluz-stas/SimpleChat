using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Entities;

namespace SimpleChat.Dal
{
    public class SimpleChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        
        public SimpleChatDbContext(DbContextOptions<SimpleChatDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired();
            });

            modelBuilder.Entity<UserChat>()
                .HasKey(t => new { t.UserId, t.ChatId });

            modelBuilder.Entity<UserChat>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserChats)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserChat>()
                .HasOne(sc => sc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(sc => sc.ChatId);
        }
    }
}
