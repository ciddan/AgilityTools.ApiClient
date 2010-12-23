using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses.Converters
{
    public class UnlinkResultResponseConverter : AdsmlResultResponseConverter<UnlinkResponse>
    {
        public UnlinkResultResponseConverter() {
            this.ElementName = "UnlinkResponse";
        }

        public override UnlinkResponse ConvertSingle(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            source.ValidateAdsmlResponse();
            CheckResponse(source);

            return base.ConvertSingle(source);
        }

        public override IEnumerable<UnlinkResponse> ConvertMultiple(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            source.ValidateAdsmlResponse();
            CheckResponse(source);
            
            return base.ConvertMultiple(source);
        }

        private static void CheckResponse(XElement source) {
            if (source.Descendants("UnlinkResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid UnlinkResponse.");
            }
        }
    }
}