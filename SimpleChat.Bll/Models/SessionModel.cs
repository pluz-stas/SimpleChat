using System;

namespace SimpleChat.Bll.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public DateTime Expires { get; set; }

        public UserModel User;
    }
}