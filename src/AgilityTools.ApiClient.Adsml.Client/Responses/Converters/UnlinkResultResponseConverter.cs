using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class UnlinkResultResponseConverter : AdsmlResultResponseConverter<UnlinkResponse>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UnlinkResultResponseConverter() {
            this.ElementName = "UnlinkResponse";
        }

        /// <summary>
        /// Converts the <see cref="XElement"/> into a <see cref="UnlinkResponse"/>.
        /// </summary>
        /// <param name="source">Required. The response to convert.</param>
        /// <returns>An <see cref="UnlinkResponse"/>.</returns>
        protected override UnlinkResponse ConvertSingle(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            return base.ConvertSingle(source);
        }

        /// <summary>
        /// Converts the <see cref="XElement"/> into an <see cref="IEnumerable{T}"/> of <see cref="UnlinkResponse"/>.
        /// </summary>
        /// <param name="source">Required. The response to convert.</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="UnlinkResponse"/></returns>
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