using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses.Converters
{
    public class DeleteResponseConverter : AdsmlResponseConverter<DeleteResponse>
    {
        public DeleteResponseConverter() {
            this.ElementName = "DeleteResponse";
        }

        public override DeleteResponse Convert(XElement source) {
            if (source == null) 
                throw new ArgumentNullException("source");

            source.ValidateAdsmlResponse();
            CheckResponse(source);

            return base.Convert(source);
        }

        private static void CheckResponse(XElement source) {
            if (source.Descendants("DeleteResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid DeleteResponse.");
            }
        }
    }
}