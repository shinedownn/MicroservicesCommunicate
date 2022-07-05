
using ContactMicroservice.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContactMicroservice.Entities.Dtos.Contact;

namespace ContactMicroservice.Entities.Dtos.Person
{
    public class PersonDto : IDto
    {
        public PersonDto()
        {
            Contacts = new HashSet<ContactDto>();
        }
        [Key]
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        [InverseProperty(nameof(ContactDto))]
        public virtual ICollection<ContactDto> Contacts { get; set; }
    }
}
