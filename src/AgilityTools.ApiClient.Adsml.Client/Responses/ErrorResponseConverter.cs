using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ErrorResponseConverter : IResponseConverter<XElement, ErrorResponse> 
    {
        public ErrorResponse Convert(XElement source) {
            return new ErrorResponse(ErrorResponse.ErrorTypes.ObjectNotFound, "1", "foo", "bar");
        }
    }
}