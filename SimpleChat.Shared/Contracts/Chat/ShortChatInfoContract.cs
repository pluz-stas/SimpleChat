namespace SimpleChat.Shared.Contracts.Chat
{
    public class ShortChatInfoContract
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
    }
}
