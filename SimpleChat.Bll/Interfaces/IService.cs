using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Interfaces
{
    public interface IService<TModel> where TModel : class, new()
    {
        Task<IEnumerable<TModel>> GetAllAsync(int skip, int top);

        Task<TModel> GetByIdAsync(int id);

        Task<int> CreateAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
    }
}
