using MediKeeper.RestApi.Entities;
using MediKeeper.RestApi.Pagination;
using System.Threading.Tasks;

namespace MediKeeper.RestApi.Repositories
{
    public interface IItemGroupRepository
    {
        void Create(Item item);
        bool Delete(Item item);
        Task<PagedList<Item>> Get(ItemQueryParameters queryParameters = null);
        Task<Item> Get(int id);
        Task Save();
        bool Update(Item item);
    }
}