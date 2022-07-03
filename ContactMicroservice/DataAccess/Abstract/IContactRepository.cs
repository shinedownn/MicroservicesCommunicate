using ContactMicroservice.Models;

namespace ContactMicroservice.DataAccess.Abstract
{
    public interface IContactRepository : IEntityRepository<Contact>
    {
    }
}
