using System.Collections.Generic;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ErrorResponse
    {
        public ErrorTypes ErrorType { get; private set; }
        public string ErrorId { get; private set; }
        public string Message { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<dynamic> Details { get; set; }

        public ErrorResponse(ErrorTypes errorType, string errorId, string message, string description) {
            ErrorType = errorType;
            ErrorId = errorId;
            Message = message;
            Description = description;
        }

        public override string ToString() {
            string errMsg = string.Format("ErrorType: {0}, id: {1}, desc: {2}. Message: {3}.",
                                          this.ErrorType, this.ErrorId, this.Description, this.Message);

            if (this.Details != null && this.Details.Count() >= 1) {
                errMsg += "Details: ";

                // Adds the result of ToString on all members of the Details collection to errMsg.
                errMsg = Details.Select(d => d.ToString())
                                .Aggregate(errMsg, (current, detail) => current + (detail + " "));
            }

            return errMsg;
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