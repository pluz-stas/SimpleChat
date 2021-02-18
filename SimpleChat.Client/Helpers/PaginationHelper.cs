using System;
using Microsoft.JSInterop;

namespace SimpleChat.Client.Helpers
{
    public class PaginationHelper
    {
        public PaginationHelper(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        [JSInvokable]
        public void SayHello()
        {
            Console.WriteLine($"Hello {Name}");
        }
    }
}