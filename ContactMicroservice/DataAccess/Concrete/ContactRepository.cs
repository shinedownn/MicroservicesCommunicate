using ContactMicroservice.Contexts;
using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Models;

namespace ContactMicroservice.DataAccess.Concrete
{
    public class ContactRepository : EfEntityRepositoryBase<Contact, ContactDbContext>, IContactRepository
    {
        public ContactRepository(ContactDbContext context) : base(context)
        {
        }
    }
}
