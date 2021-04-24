using System;

namespace SimpleChat.Client.Services
{
    public class ChatTracker
    {
        private int selectedChatId;

        public Action ChatSelected;

        public int SelectedChatId => selectedChatId;

        public void SelectChat(int chatId)
        {
            selectedChatId = chatId;
            ChatSelected?.Invoke();
        }
    }
}
