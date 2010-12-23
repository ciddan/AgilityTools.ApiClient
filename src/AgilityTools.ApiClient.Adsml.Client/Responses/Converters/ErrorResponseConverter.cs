using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses.Converters
{
    public class ErrorResponseConverter : IResponseConverter<XElement, ErrorResponse> 
    {
        public ErrorResponse Convert(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            source.ValidateAdsmlResponse();
            
            if (source.Descendants("ErrorResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid ErrorResponse.");   
            }

            var errorType = (string) source.Descendants("ErrorResponse").First().Attribute("type");
            errorType = errorType.Capitalize();

            XElement response = source.Descendants("ErrorResponse").First();

            return new ErrorResponse
                   {
                       Description = (string) response.Attribute("description"),
                       ErrorId = (string) response.Attribute("id"),
                       ErrorType = (ErrorResponse.ErrorTypes) Enum.Parse(typeof (ErrorResponse.ErrorTypes), errorType),
                       Message = response.Descendants("Message").First().Value
                   };
        }
    }
}