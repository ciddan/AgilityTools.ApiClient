using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class DeleteResultResponseConverter : AdsmlResultResponseConverter<DeleteResponse>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DeleteResultResponseConverter(string validationDocument) : base(validationDocument) {
            this.ElementName = "DeleteResponse";
        }

        /// <summary>
        /// Converts the <see cref="XElement"/> into an <see cref="IEnumerable{T}"/> of <see cref="DeleteResponse"/>.
        /// </summary>
        /// <param name="source">Required. The response to convert.</param>
        /// <returns></returns>
        public override IEnumerable<DeleteResponse> Convert(XElement source) {
            if (source == null)
                throw new ArgumentNullException("source");

            CheckResponse(source);

            return base.Convert(source);
        }

        private void CheckResponse(XElement source) {
            source.ValidateAdsmlResponse(_validationDocument);

            if (!source.Descendants("DeleteResponse").Any()) {
                throw new InvalidOperationException("Not a valid DeleteResponse.");
            }
        }
    }
}