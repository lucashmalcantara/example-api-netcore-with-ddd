using DddExample.Domain.CustomerContext.ValueObjects;

namespace DddExample.Domain.CustomerContext.Entities
{
    public class Customer
    {
        public Name Name { get; set; }
        public Cpf Cpf { get; set; }
        public Email Email { get; set; }

        private string _segment;
        public string Segment
        {
            get { return _segment; }
            set { _segment = value?.ToUpper(); }
        }


        public Customer(Name name, Cpf cpf, Email email)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
        }
    }
}
