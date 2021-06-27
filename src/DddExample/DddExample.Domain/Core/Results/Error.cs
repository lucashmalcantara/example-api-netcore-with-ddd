namespace DddExample.Domain.Core.Results
{
    public class Error
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }

        public Error(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }

        public Error(string message)
        {
            PropertyName = null;
            Message = message;
        }
    }
}
