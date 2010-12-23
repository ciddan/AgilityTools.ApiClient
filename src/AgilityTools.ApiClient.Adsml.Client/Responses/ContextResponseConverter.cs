using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ContextResponseConverter : IResponseConverter<XElement, ContextResponse>
    {
        public ContextResponse Convert(XElement source) {
            source.ValidateAdsmlResponse();
            CheckResponse(source);

            return new ContextResponse();
        }

        private static void CheckResponse(XElement source) {
            if ((source.Descendants("StructureContext").Count() < 1) && (source.Descendants("Context").Count() < 1)) {
                throw new InvalidOperationException("Not a valid ContextResponse.");
            }
        }
    }
}