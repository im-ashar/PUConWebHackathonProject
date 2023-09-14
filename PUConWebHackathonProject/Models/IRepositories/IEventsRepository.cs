using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PUConWebHackathonProject.Models.IRepositories
{
    public interface IEventsRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        Task<int> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
