using System;
using System.Runtime.Serialization;
using AgilityTools.ApiClient.Adsml.Client.Responses;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [Serializable]
    public class AdsmlException : Exception, ISerializable
    {
        private ErrorResponse _errorResponse;
        public ErrorResponse ErrorResponse {
            get { return _errorResponse; }
            set { _errorResponse = value; }
        }

        public AdsmlException() {
        }

        public AdsmlException(string message, ErrorResponse errorResponse = null)
            : base(message) {
            _errorResponse = errorResponse;
        }

        public AdsmlException(string message, Exception inner, ErrorResponse errorResponse = null)
            : base(message, inner) {
            _errorResponse = errorResponse;
        }

        protected AdsmlException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}