using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Interfaces
{
    public interface ISessionService : IService<SessionModel, Session>
    {
    }
}