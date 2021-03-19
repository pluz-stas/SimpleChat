using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Helpers;

namespace SimpleChat.Dal
{
    public class SimpleChatDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        
        public SimpleChatDbContext(DbContextOptions<SimpleChatDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
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

                entity.Property(e => e.UserName)
                    .IsRequired();
            });

            modelBuilder.Seed();
        }
    }
}
