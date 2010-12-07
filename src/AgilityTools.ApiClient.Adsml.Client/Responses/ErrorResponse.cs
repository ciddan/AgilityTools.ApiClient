namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ErrorResponse
    {
        public ErrorTypes ErrorType { get; internal set; }
        public string ErrorId { get; internal set; }
        public string Message { get; internal set; }
        public string Description { get; internal set; }

        internal ErrorResponse() {
        }

        public ErrorResponse(ErrorTypes errorType, string errorId, string message, string description) {
            ErrorType = errorType;
            ErrorId = errorId;
            Message = message;
            Description = description;
        }

        public override string ToString() {
            return string.Format("ErrorType: {0}, id: {1}, desc: {2}. Message: {3}.",
                                 this.ErrorType, this.ErrorId, this.Description, this.Message);
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