using System;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class DeleteResponseConverter : AdsmlResponseConverter<DeleteResponse>
    {
        public override DeleteResponse Convert(XElement source) {
            if (source == null) 
                throw new ArgumentNullException("source");

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