using ContactMicroservice.Entities.Concrete; 

namespace ContactMicroservice.DataAccess.Abstract
{
    public interface IPersonRepository : IEntityRepository<Person>
    {
    }
}
