namespace DddExample.Domain.CustomerContext.ValueObjects
{
    public readonly struct Phone
    {
        public int AreaCode { get; }
        public string Number { get; }
        public string FullNumber { get { return $"{AreaCode}{Number}"; } }

        public Phone(int areaCode, string number)
        {
            AreaCode = areaCode;
            Number = number;
        }

        public override string ToString()
        {
            return FullNumber;
        }
    }
}
