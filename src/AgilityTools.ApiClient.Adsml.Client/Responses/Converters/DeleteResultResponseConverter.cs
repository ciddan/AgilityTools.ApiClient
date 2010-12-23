using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses.Converters
{
    public class DeleteResultResponseConverter : AdsmlResultResponseConverter<DeleteResponse>
    {
        public DeleteResultResponseConverter() {
            this.ElementName = "DeleteResponse";
        }

        public override DeleteResponse ConvertSingle(XElement source) {
            if (source == null) 
                throw new ArgumentNullException("source");

            source.ValidateAdsmlResponse();
            CheckResponse(source);

            return base.ConvertSingle(source);
        }

        public override IEnumerable<DeleteResponse> ConvertMultiple(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            source.ValidateAdsmlResponse();
            CheckResponse(source);

            return base.ConvertMultiple(source);
        }

        private static void CheckResponse(XElement source) {
            if (source.Descendants("DeleteResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid DeleteResponse.");
            }
        }
    }
}