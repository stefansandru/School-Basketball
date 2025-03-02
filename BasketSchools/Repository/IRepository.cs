namespace BasketSchools.Repository;

using Domain;
using System.Collections.Generic;

public interface IRepository<TId, TEntity> where TEntity : IEntity<TId>
{
    TEntity? FindOne(TId id);
    IEnumerable<TEntity>? FindAll();
    TEntity? Save(TEntity team);
    TEntity? Delete(TId id);
    TEntity? Update(TEntity team);
}