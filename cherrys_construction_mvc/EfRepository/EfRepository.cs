using Ardalis.Specification.EntityFrameworkCore;
using cherrys_construction_mvc.Data;
using cherrys_construction_mvc.EfRepository.Interfaces;

namespace cherrys_construction_mvc.EfRepository
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IEfRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
