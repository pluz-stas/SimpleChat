using System.Threading.Tasks;

namespace SimpleChat.Bll.Interfaces
{
    public interface IPasswordService
    {
        string Hash(string password);

        bool Verify(string passwordHash, string password);
    }
}