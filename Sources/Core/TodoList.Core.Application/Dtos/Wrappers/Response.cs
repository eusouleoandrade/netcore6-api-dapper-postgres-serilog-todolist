using System.Text.Json.Serialization;
using TodoList.Infra.Notification.Models;

namespace TodoList.Core.Application.Dtos.Wrappers
{
    public class Response<TData>
        where TData : class

    {
        private const string succeededMessage = "Request processed";

        private const string failedMessage = "Failed to process request";

        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<NotificationMessage>? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; set; }

        public Response(TData data, bool succeeded, string? message = null, IEnumerable<NotificationMessage>? errors = null)
        {
            Succeeded = succeeded;
            Errors = errors;
            Data = data;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? succeededMessage : failedMessage;
            else
                Message = message;
        }
    }

    public class Response<TData, TErrors>
        where TData : class
        where TErrors : class

    {
        private const string succeededMessage = "Request processed";

        private const string failedMessage = "Failed to process request";

        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TErrors? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; set; }

        public Response(TData data, bool succeeded, string? message = null, TErrors? errors = null)
        {
            Succeeded = succeeded;
            Errors = errors;
            Data = data;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? succeededMessage : failedMessage;
            else
                Message = message;
        }
    }

    public class Response
    {
        private const string succeededMessage = "Request processed";

        private const string failedMessage = "Failed to process request";

        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<NotificationMessage>? Errors { get; set; }

        public Response(bool succeeded, string? message = null, IEnumerable<NotificationMessage>? errors = null)
        {
            Succeeded = succeeded;
            Errors = errors;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? succeededMessage : failedMessage;
            else
                Message = message;
        }
    }
}
