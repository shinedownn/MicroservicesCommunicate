using ContactMicroservice.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactMicroservice.Entities.Concrete
{
    [Table("Person")]
    public partial class Person : IEntity
    {
        public Person()
        {
            Contacts = new HashSet<Contact>();
        }
        [Key]
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }


        [InverseProperty(nameof(Contact.Person))]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
