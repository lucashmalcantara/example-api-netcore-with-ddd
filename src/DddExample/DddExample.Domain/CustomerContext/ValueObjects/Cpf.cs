namespace DddExample.Domain.CustomerContext.ValueObjects
{
    public readonly struct Cpf
    {
        public string Document { get; }

        public Cpf(string document)
        {
            Document = document;
        }

        public override string ToString()
        {
            return Document;
        }
    }
}
