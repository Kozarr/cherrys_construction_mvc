using Ardalis.Specification;

namespace cherrys_construction_mvc.EfRepository.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
