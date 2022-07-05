using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Entities.Concrete;

namespace ContactMicroservice.DataAccess.Concrete
{
    public class ContactRepository : EfEntityRepositoryBase<Contact, ContactMicroserviceContext>, IContactRepository
    { 
        public ContactRepository(ContactMicroserviceContext context) : base(context)
        {
        }
    }
}
