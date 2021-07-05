using System.Text.Json.Serialization;

namespace DddExample.Domain.Core.Results
{
    public class Error
    {
        [JsonPropertyName("propertyName")]
        public string PropertyName { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonConstructor]
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

        private Error() { }
    }
}
