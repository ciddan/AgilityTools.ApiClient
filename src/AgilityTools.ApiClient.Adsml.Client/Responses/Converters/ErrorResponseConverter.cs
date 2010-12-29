using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses.Converters
{
    public class ErrorResponseConverter : IResponseConverter<XElement, ErrorResponse> 
    {
        private static ErrorResponse ConvertSingle(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            CheckResponse(source);

            var errorResponse = source.Descendants("ErrorResponse").SingleOrDefault();

            if (errorResponse == null) {
                return null;
            }

            var errorType = (string) errorResponse.Attribute("type");
            errorType = errorType.Capitalize();

            return new ErrorResponse
                   {
                       Description = (string) errorResponse.Attribute("description"),
                       ErrorId = (string) errorResponse.Attribute("id"),
                       ErrorType = (ErrorResponse.ErrorTypes) Enum.Parse(typeof(ErrorResponse.ErrorTypes), errorType),
                       Message = errorResponse.Descendants("Message").First().Value
                   };
        }

        public IEnumerable<ErrorResponse> Convert(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            CheckResponse(source);

            return source.Descendants("ErrorResponse").Select(ConvertSingle);
        }

        private static void CheckResponse(XElement source) {
            source.ValidateAdsmlResponse();

            if (source.Descendants("ErrorResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid ErrorResponse.");
            }
        }
    }
}