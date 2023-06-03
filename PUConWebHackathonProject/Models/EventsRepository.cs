using Microsoft.EntityFrameworkCore;
using PUConWebHackathonProject.Models.IRepositories;

namespace PUConWebHackathonProject.Models
{
    public class EventsRepository<T>:IEventsRepository<T> where T :class
    {
            private readonly EventsContext _context;

            public EventsRepository(EventsContext context)
            {
                _context = context;
            }

            public T GetById(int id)
            {
                return _context.Set<T>().Find(id);
            }

            public IEnumerable<T> GetAll()
            {
                return _context.Set<T>().ToList();
            }

            public void Add(T entity)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
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
