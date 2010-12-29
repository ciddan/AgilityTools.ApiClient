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

        protected override UnlinkResponse ConvertSingle(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            return base.ConvertSingle(source);
        }

        public override IEnumerable<UnlinkResponse> Convert(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            CheckResponse(source);
            
            return base.Convert(source);
        }

        private static void CheckResponse(XElement source) {
            source.ValidateAdsmlResponse();

            if (source.Descendants("UnlinkResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid UnlinkResponse.");
            }
        }
    }
}