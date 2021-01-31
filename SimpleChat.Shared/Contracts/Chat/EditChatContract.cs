namespace SimpleChat.Shared.Contracts.Chat
{
    public class EditChatContract
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
    }
}
