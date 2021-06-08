namespace DddExample.Domain.CustomerContext.ValueObjects
{
    public readonly struct PersonName
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public PersonName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
