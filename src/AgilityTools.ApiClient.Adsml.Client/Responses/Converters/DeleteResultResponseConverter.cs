using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class DeleteResultResponseConverter : AdsmlResultResponseConverter<DeleteResponse>
    {
        public DeleteResultResponseConverter() {
            this.ElementName = "DeleteResponse";
        }

        public override IEnumerable<DeleteResponse> Convert(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            CheckResponse(source);

            return base.Convert(source);
        }

        private static void CheckResponse(XElement source) {
            source.ValidateAdsmlResponse();

            if (source.Descendants("DeleteResponse").Count() < 1) {
                throw new InvalidOperationException("Not a valid DeleteResponse.");
            }
        }
    }
}