using Data.Contexts;

namespace Data.Repositories;

public class GenericRepository<TEntity> : BaseRepository<TEntity> where TEntity : class
{
    public GenericRepository(AppDbContext context) : base(context) { }
}
