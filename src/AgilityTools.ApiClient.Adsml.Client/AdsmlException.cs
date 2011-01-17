using System;
using System.Runtime.Serialization;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [Serializable]
    public class AdsmlException : Exception, ISerializable
    {
        public AdsmlException() {
        }

        public AdsmlException(string message)
            : base(message) {
        }

        public AdsmlException(string message, Exception inner)
            : base(message, inner) {
        }

        protected AdsmlException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}