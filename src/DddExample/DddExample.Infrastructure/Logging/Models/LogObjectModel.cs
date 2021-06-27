using System.Text.Encodings.Web;
using System.Text.Json;

namespace DddExample.Infrastructure.Logging.Models
{
    public class LogObjectModel
    {
        public string CorrelationId { get; protected set; }
        public string Action { get; protected set; }
        public string Message { get; protected set; }
        public string Data { get; protected set; }

        public LogObjectModel(
            string correlationId,
            string action,
            string message,
            object data)
        {
            CorrelationId = correlationId;
            Action = action;
            Message = message;
            Data = ConvertDataToString(data);
        }

        private string ConvertDataToString(object data)
        {
            if (data == null)
                return null;

            var options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            return JsonSerializer.Serialize(data, options);
        }
    }
}
