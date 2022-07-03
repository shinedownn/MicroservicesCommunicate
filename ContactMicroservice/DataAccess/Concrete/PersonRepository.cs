using ContactMicroservice.Contexts;
using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Models;

namespace ContactMicroservice.DataAccess.Concrete
{
    public class PersonRepository : EfEntityRepositoryBase<Person, ContactDbContext>, IPersonRepository
    {
        public PersonRepository(ContactDbContext context) : base(context)
        {
        }
    }
}
