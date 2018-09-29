using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ModernGF.Shared.Entities;
using System;
using System.Linq;

namespace ModernGF.Shared.Repositories
{
    public abstract class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity>
        where TEntity : Entity<int>
    {
        protected DbContext _context;

        public virtual TEntity FindById(int id)
        {
            //return DecryptEntity(Context.Set<TEntity>().Find(id));
            return _context.Set<TEntity>().Find(id);
        }


        public virtual TEntity CreateOrUpdate(TEntity entity)
        {
            if (entity.Cod == 0)
            {
                entity.DataCadastro = DateTime.Now;
                entity = this.Create(entity);
            }
            else
            {
                var baseEntity = _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Cod == entity.Cod);
                entity.DataAlteracao = DateTime.Now;

                EntityEntry<TEntity> entityEntry = _context.Entry(entity);

                if (entityEntry.State == EntityState.Detached)
                {
                    _context.Set<TEntity>().Attach(entity);

                    entity.DataCadastro = baseEntity.DataCadastro;
                    entityEntry.State = EntityState.Modified;
                }
            }

            this.SaveChanges();
            return entity;
        }
   
        public virtual TEntity Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public virtual void Delete(long id)
        {
            var item = _context.Set<TEntity>().Find(id);

            item.DataExcluido = DateTime.Now;

            this.CreateOrUpdate(item);

            this.SaveChanges();

            //Context.Set<TEntity>().Remove(item);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Releases all resources used by the Entities
        /// </summary>
        public void Dispose()
        {
            if (null != _context)
            {
                _context.Dispose();
            }
        }
        public virtual bool SaveChanges() => 0 < _context.SaveChanges();

    }
}
