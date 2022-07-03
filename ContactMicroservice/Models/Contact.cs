using ContactMicroservice.DataAccess;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactMicroservice.Models
{
    public partial class Contact : IEntity
    {
        public Guid? Personid { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }

        public virtual Person Person { get; set; } = new();
    }
}
