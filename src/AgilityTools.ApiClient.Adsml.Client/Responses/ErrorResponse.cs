using System;
using System.Runtime.Serialization;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    [Serializable]
    public class ErrorResponse : ISerializable
    {
        public ErrorTypes ErrorType { get; internal set; }
        public string ErrorId { get; internal set; }
        public string Message { get; internal set; }
        public string Description { get; internal set; }

        public ErrorResponse() {
        }

        public ErrorResponse(ErrorTypes errorType, string errorId, string message, string description) {
            ErrorType = errorType;
            ErrorId = errorId;
            Message = message;
            Description = description;
        }

        protected ErrorResponse(SerializationInfo info, StreamingContext context) {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            this.ErrorId = (string) info.GetValue("ErrorId", typeof (string));
            this.Message = (string) info.GetValue("Message", typeof(string));
            this.Description = (string) info.GetValue("Description", typeof(string));
            this.ErrorType = (ErrorTypes) info.GetValue("ErrorType", typeof (ErrorTypes));
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

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ErrorId", this.ErrorId);
            info.AddValue("Message", this.Message);
            info.AddValue("Description", this.Description);
            info.AddValue("ErrorType", this.ErrorType);
        }
    }
}