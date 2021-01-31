namespace SimpleChat.Shared.Contracts.Chat
{
    public class CreateChatContract
    {
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
    }
}
