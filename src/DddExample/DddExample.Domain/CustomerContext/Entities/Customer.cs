using DddExample.Domain.CustomerContext.ValueObjects;
using System;

namespace DddExample.Domain.CustomerContext.Entities
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public PersonName Name { get; set; }
        public Cpf Cpf { get; set; }
        public Phone Phone { get; set; }
        public Email Email { get; set; }
        public DateTime Birthdate { get; set; }

        public Customer(
            PersonName name,
            Cpf cpf,
            Phone phone,
            Email email,
            DateTime birthdate)
        {
            Name = name;
            Cpf = cpf;
            Phone = phone;
            Email = email;
            Birthdate = birthdate;
        }
    }
}
