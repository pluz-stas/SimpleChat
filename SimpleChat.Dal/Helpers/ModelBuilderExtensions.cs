using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Entities;
using System;

namespace SimpleChat.Dal.Helpers
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>().HasData(
                    new Chat
                    {
                        Id = 1,
                        IsPublic = true,
                        Name = "Cool chat",
                    },
                    new Chat
                    {
                        Id = 2,
                        IsPublic = true,
                        Name = "Another one cool chat",
                    });

            modelBuilder.Entity<Message>().HasData(
                new Message
                {
                    Id = 1,
                    ChatId = 1,
                    Content = "Hi all!",
                    CreatedDate = new DateTime(1999, 04, 04),
                    IsRead = false,
                    UserName = "Danylo"
                },
                new Message
                {
                    Id = 2,
                    ChatId = 1,
                    Content = "This is my chat)",
                    CreatedDate = new DateTime(1999, 04, 04, 4, 21, 8),
                    IsRead = false,
                    UserName = "Danylo"
                },
                new Message
                {
                    Id = 3,
                    ChatId = 2,
                    Content = "This is my another chat",
                    CreatedDate = new DateTime(1999, 04, 04),
                    IsRead = false,
                    UserName = "Danylo"
                });
        }
    }
}
