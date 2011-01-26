using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AgilityTools.ApiClient.Adsml.Client.Responses;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [Serializable]
    public class AdsmlException : Exception, ISerializable
    {
        private IEnumerable<ErrorResponse> _errorResponses;
        public IEnumerable<ErrorResponse> ErrorResponses {
            get { return _errorResponses; }
            set { _errorResponses = value; }
        }

        public AdsmlException(IEnumerable<ErrorResponse> errorResponses = null) {
            _errorResponses = errorResponses;
        }

        public AdsmlException(string message, IEnumerable<ErrorResponse> errorResponses = null)
            : base(message) {
            _errorResponses = errorResponses;
        }

        public AdsmlException(string message, Exception inner, IEnumerable<ErrorResponse> errorResponses = null)
            : base(message, inner) {
            _errorResponses = errorResponses;
        }

        protected AdsmlException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}