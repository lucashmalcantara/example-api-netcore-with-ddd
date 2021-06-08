using DddExample.Domain.CustomerContext.ValueObjects;
using System;

namespace DddExample.Domain.CustomerContext.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public PersonName Name { get; set; }
        public Cpf Cpf { get; set; }
        public Email Email { get; set; }
        public DateTime Birthdate { get; set; }

        public Customer(
            Guid id,
            PersonName name,
            Cpf cpf,
            Email email,
            DateTime birthdate)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Email = email;
            Birthdate = birthdate;
        }
    }
}
