namespace SimpleChat.Shared.Contracts.Message
{
    public class EditMessageContract
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ChatId { get; set; }
    }
}
