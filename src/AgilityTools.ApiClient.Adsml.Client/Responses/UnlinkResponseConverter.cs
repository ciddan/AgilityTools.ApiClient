using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class UnlinkResponseConverter : AdsmlResponseConverter<UnlinkResponse>
    {
        public UnlinkResponseConverter() {
            this.ElementName = "UnlinkResponse";
        }

        public override UnlinkResponse Convert(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            source.ValidateAdsmlResponse();
            CheckResponse(source);

            return base.Convert(source);
        }

        private static void CheckResponse(XElement source) {
            if (source.Descendants("UnlinkResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid UnlinkResponse.");
            }
        }
    }
}