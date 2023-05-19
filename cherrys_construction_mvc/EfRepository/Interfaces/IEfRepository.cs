using Ardalis.Specification;

namespace cherrys_construction_mvc.EfRepository.Interfaces
{
    public interface IEfRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
