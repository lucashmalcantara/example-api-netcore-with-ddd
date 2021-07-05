using System;

namespace DddExample.Api.V1.Models.CustomerContext
{
    public class CustomerGetResultModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public int PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
