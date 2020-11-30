using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    public class SessionService : AbstractService<SessionModel, Session>, ISessionService
    {
        public SessionService(ISessionRepository repository) : base(repository) { }
    }
}
