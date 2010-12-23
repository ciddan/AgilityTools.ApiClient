using System;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public static class ResponseAdaptors
    {
        public static CreateResponse ToCreateResponse(this XElement response)
        {
            IsAdsmlRepsonse(response);

            CreateResponse createResponse = ToBasicResponse(response);

            if (response.Descendants("StructureContext").FirstOrDefault() != null)
            {
                var contextNode = response.Descendants("StructureContext").FirstOrDefault();

                // ReSharper disable PossibleNullReferenceException
                string idPath = contextNode.Attribute("idPath").Value;
                createResponse.CreatedObjectId = int.Parse(idPath.Split(':').Last());
                createResponse.CreatedObjectPath = contextNode.Attribute("name").Value;
                // ReSharper restore PossibleNullReferenceException
            }

            return createResponse;
        }

        public static AdsmlResponse ToBasicResponse(this XElement response)
        {
            IsAdsmlRepsonse(response);

            var adsmlResponse = new AdsmlResponse
                                {
                                    RequestResponse = response
                                };

            if (response.Descendants("ErrorResponse").FirstOrDefault() != null)
            {
                var errorNode = response.Descendants("ErrorResponse").FirstOrDefault();

                // ReSharper disable PossibleNullReferenceException
                adsmlResponse.ErrorId = int.Parse(errorNode.Attribute("id").Value);
                adsmlResponse.ErrorType = errorNode.Attribute("type").Value;
                adsmlResponse.ErrorMessage = errorNode.Descendants("Message").Single().Value;
                // ReSharper restore PossibleNullReferenceException
            }

            return adsmlResponse;
        }

        private static void IsAdsmlRepsonse(XElement response) {
            if (response.Name != "BatchResponse")
                throw new InvalidOperationException("Not a valid Adsml response.");
        }
    }
}