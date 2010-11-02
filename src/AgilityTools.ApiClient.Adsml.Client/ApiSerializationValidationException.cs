using System;
using System.Runtime.Serialization;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [Serializable]
    public class ApiSerializationValidationException : Exception
    {
        public ApiSerializationValidationException() {
        }

        public ApiSerializationValidationException(string message) : base(message) {
        }

        public ApiSerializationValidationException(string message, Exception inner) : base(message, inner) {
        }

        protected ApiSerializationValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}