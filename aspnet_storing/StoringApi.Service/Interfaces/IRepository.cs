using System.Collections.Generic;
using StoringApi.Abstracts;

namespace StoringApi.Service.Interfaces
{
  public interface IRepository
  {
    public IEnumerable<T> GetAll<T>() where T : AEntity;

    public T Find<T>(long id) where T : AEntity;

    public void Add<T>(T item) where T : AEntity;

    public void Remove<T>(T item) where T : AEntity;

    public bool RemoveByID<T>(long id) where T : AEntity;

    public void Save();
  }
}
