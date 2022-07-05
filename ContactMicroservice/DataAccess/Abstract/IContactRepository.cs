using ContactMicroservice.Entities.Concrete;

namespace ContactMicroservice.DataAccess.Abstract
{
    public interface IContactRepository : IEntityRepository<Contact>
    {
    }
}
