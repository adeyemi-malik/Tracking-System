using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystemBLayer.Repository
{
    public interface IRepository<T, IEntity>
    {

        Task<IEntity> Get(T Id);

        Task<IQueryable<IEntity>> All();

        Task Delete(IEntity entity);

        Task Delete(T Id);

        Task<IEntity> Update(IEntity entity);

        Task<IEntity> Add(IEntity entity);

        Task SaveChanges();
    }
}
