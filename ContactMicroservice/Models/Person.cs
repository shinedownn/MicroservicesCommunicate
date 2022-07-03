using ContactMicroservice.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactMicroservice.Models
{
    public partial class Person : IEntity
    {
        public Guid Personid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; } 
        public List<Contact> Contacts { get; set; } = new();
    }
}
