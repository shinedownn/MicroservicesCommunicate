using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Entities.Concrete; 

namespace ContactMicroservice.DataAccess.Concrete
{
    public class PersonRepository : EfEntityRepositoryBase<Person, ContactMicroserviceContext>, IPersonRepository
    {
        public PersonRepository(ContactMicroserviceContext context) : base(context)
        {
        }
    }
}
