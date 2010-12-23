using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public interface IAdsmlResponse
    {
        XElement RequestResponse { get; set; }
        string ErrorMessage { get; set; }
        int ErrorId { get; set; }
        string ErrorType { get; set; }
    }

    public class AdsmlResponse : IAdsmlResponse
    {
        public XElement RequestResponse { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorId { get; set; }
        public string ErrorType { get; set; }

        public static implicit operator CreateResponse(AdsmlResponse adsmlResponse) {
            return new CreateResponse
                   {
                       ErrorId = adsmlResponse.ErrorId,
                       ErrorMessage = adsmlResponse.ErrorMessage,
                       ErrorType = adsmlResponse.ErrorType,
                       RequestResponse = adsmlResponse.RequestResponse
                   };
        }
    }

    public class CreateResponse : IAdsmlResponse
    {
        public XElement RequestResponse { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorId { get; set; }
        public string ErrorType { get; set; }
        public int CreatedObjectId { get; set; }
        public string CreatedObjectPath { get; set; }

        public static explicit operator AdsmlResponse(CreateResponse createResponse) {
            return new AdsmlResponse
                   {
                       ErrorId = createResponse.ErrorId,
                       ErrorMessage = createResponse.ErrorMessage,
                       ErrorType = createResponse.ErrorType,
                       RequestResponse = createResponse.RequestResponse
                   };
        }
    }
}