namespace SimpleChat.Client.Shared
{
    public partial class MainLayout
    {
        private bool isChatSidebarOpen;
        private bool isUserSidebarOpen;

        private void ToggleChats()
        {
            isChatSidebarOpen = !isChatSidebarOpen;
            isUserSidebarOpen = !isChatSidebarOpen && isUserSidebarOpen;
        }

        private void ToggleUser()
        {
            isUserSidebarOpen = !isUserSidebarOpen;
            isChatSidebarOpen = isChatSidebarOpen && !isUserSidebarOpen;
        }
    }
}
