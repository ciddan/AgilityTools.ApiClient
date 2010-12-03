namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ErrorResponse
    {
        public ErrorTypes ErrorType { get; private set; }
        public string ErrorId { get; private set; }
        public string Message { get; private set; }
        public string Description { get; private set; }
        public dynamic[] Details { get; set; }

        internal ErrorResponse(ErrorTypes errorType, string errorId, string message, string description) {
            ErrorType = errorType;
            ErrorId = errorId;
            Message = message;
            Description = description;
        }

        public enum ErrorTypes
        {
            MalformedRequest,
            ApplicationError,
            SystemError,
            ObjectNotFound
        }
    }
}