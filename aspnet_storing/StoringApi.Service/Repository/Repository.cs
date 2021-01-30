using System.Collections.Generic;
using System.Linq;
using StoringApi.Abstracts;
using StoringApi.Service.Interfaces;

namespace StoringApi.Service.Repository
{
  public class Repository : IRepository
  {
    private VWFContext _context;

    public Repository(VWFContext context)
    {
      _context = context;
    }

    public void Add<T>(T item) where T : AEntity
    {
      _context.Set<T>().Add(item);
    }

    public T Find<T>(long id) where T : AEntity
    {
      return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll<T>() where T : AEntity
    {
      return _context.Set<T>();
    }

    public void Remove<T>(T item) where T : AEntity
    {
      _context.Set<T>().Remove(item);
    }

    public bool RemoveByID<T>(long id) where T : AEntity
    {
      var item = _context.Set<T>().FirstOrDefault(entity => entity.EntityID == id);
      if(item != null)
      {
        _context.Remove(item);
        return true;
      }
      return false;
    }

    public void Save()
    {
      _context.SaveChanges();
    }
  }
}
