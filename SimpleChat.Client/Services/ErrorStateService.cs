using System;

namespace SimpleChat.Client.Services
{
    public class ErrorStateService
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        public event Action OnChange;

        public void SetError(string title, string message)
        {
            Message = message;
            Title = title;
            NotifyStateChanged();
        }

        public void ClearError()
        {
            Message = null;
            Title = null;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}