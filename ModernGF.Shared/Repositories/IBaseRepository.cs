namespace ModernGF.Shared.Repositories
{
    public interface IBaseRepository<TEntity>
    {

        /// <summary>
        /// Find Entity by Id.
        /// </summary>
        TEntity FindById(int id);
        /// <summary>
        /// Creates a new empty entity.
        /// </summary>
        //TEntity Create();

        /// <summary>
        /// Creates the existing entity.
        /// </summary>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Creates or Update entity.
        /// </summary>
        TEntity CreateOrUpdate(TEntity entity);

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Delete an entity using its primary key.
        /// </summary>
        void Delete(long id);

        /// <summary>
        /// Delete the given entity.
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// Save any changes to the TContext
        /// </summary>
        bool SaveChanges();
    }
}
