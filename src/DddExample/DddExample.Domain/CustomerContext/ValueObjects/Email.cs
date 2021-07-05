namespace DddExample.Domain.CustomerContext.ValueObjects
{
    public readonly struct Email
    {
        public string Address { get; }

        public Email(string address)
        {
            Address = address;
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
