using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ErrorResponseConverter : ResponseConverter<XElement, ErrorResponse>
    {
        public ErrorResponseConverter(string validationDocument) : base(validationDocument) {
        }

        /// <summary>
        /// Converts an <see cref="XElement"/> into a <see cref="ErrorResponse"/>.
        /// </summary>
        /// <param name="source">Required. The response to convert.</param>
        /// <returns>An <see cref="ErrorResponse"/>.</returns>
        private static ErrorResponse ConvertSingle(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            var errorType = (string) source.Attribute("type");
            errorType = errorType.Capitalize();

            return new ErrorResponse
                   {
                       Description = (string) source.Attribute("description"),
                       ErrorId = (string) source.Attribute("id"),
                       ErrorType = (ErrorResponse.ErrorTypes) Enum.Parse(typeof(ErrorResponse.ErrorTypes), errorType),
                       Message = source.Descendants("Message").First().Value
                   };
        }

        /// <summary>
        /// Converts an <see cref="XElement"/> into an <see cref="IEnumerable{T}"/> of <see cref="ErrorResponse"/>.
        /// </summary>
        /// <param name="source">Required. The response to convert.</param>
        /// <returns></returns>
        public override IEnumerable<ErrorResponse> Convert(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            CheckResponse(source);

            return source.Descendants("ErrorResponse").Select(ConvertSingle);
        }

        private void CheckResponse(XElement source) {
            source.ValidateAdsmlResponse(_validationDocument);

            if (!source.Descendants("ErrorResponse").Any()) {
                throw new InvalidOperationException("Not a valid ErrorResponse.");
            }
        }
    }
}