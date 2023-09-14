using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PUConWebHackathonProject.Models.IRepositories;
using PUConWebHackathonProject.Models.Repositories.Identity;

namespace PUConWebHackathonProject.Models
{
    public class EventsRepository<T> : IEventsRepository<T> where T : class
    {
        private readonly IdentityContext _context;

        public EventsRepository(IdentityContext context)
        {
            _context = context;
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<int> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            var result=await _context.SaveChangesAsync();
            return result;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
