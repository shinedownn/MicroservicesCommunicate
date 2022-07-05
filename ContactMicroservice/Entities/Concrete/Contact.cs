using ContactMicroservice.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactMicroservice.Entities.Concrete
{
    [Table("Contact")]
    [Index(nameof(PersonId), Name = "fki_fk_PersonId")]
    public partial class Contact : IEntity
    {
        [Key]
        public Guid ContactId { get; set; }
        public Guid PersonId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Contacts")]
        public virtual Person Person { get; set; }
    }
}
