using Newtonsoft.Json;

namespace TodoList.Core.Application.Dtos.Wrappers
{
    public class Response<T>
    {
        private const string succeededMessage = "Request processed";

        private const string failedMessage = "Failed to process request";

        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string>? Errors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T? Data { get; set; }

        public Response(T data, bool succeeded, string? message = null, IEnumerable<string>? errors = null)
        {
            Succeeded = succeeded;
            Errors = errors;
            Data = data;

            if (string.IsNullOrEmpty(message))
                Message = succeeded ? succeededMessage : failedMessage;
        }
    }

    public class Response
    {
        private const string succeededMessage = "Request processed";

        private const string failedMessage = "Failed to process request";

        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Errors { get; set; }

        public Response(bool succeeded, string? message, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors;

            if (string.IsNullOrEmpty(message))
                Message = succeeded ? succeededMessage : failedMessage;
        }
    }
}
