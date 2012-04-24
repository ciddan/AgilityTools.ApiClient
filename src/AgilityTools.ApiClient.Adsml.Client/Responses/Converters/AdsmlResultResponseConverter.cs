using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    /// <summary>
    /// Abstract base class for all response converters. Implements <see cref="IResponseConverter{XElement, TOutput}"/>.
    /// </summary>
    /// <typeparam name="TOutput">The type of the resulting object after the conversion.</typeparam>
    public abstract class AdsmlResultResponseConverter<TOutput> : ResponseConverter<XElement, TOutput> where TOutput : AdsmlResult, new()
    {
        protected AdsmlResultResponseConverter(string validationDocument) : base(validationDocument) {
        }

        protected string ElementName { get; set; }

        /// <summary>
        /// Used to convert an <see cref="XElement"/> source into a single <typeparamref name="TOutput"/>.
        /// </summary>
        /// <param name="source">Required. The data to convert.</param>
        /// <returns>The result of the conversion of type <typeparamref name="TOutput"/>.</returns>
        protected virtual TOutput ConvertSingle(XElement source) {
            return new TOutput
                   {
                       Code = (string) source.Attribute("code"),
                       Description = (string) source.Attribute("description"),
                       Messages = source.Descendants("Message").Select(x => x.Value).ToList()
                   };
        }

        /// <summary>
        /// Converts the input of <see cref="XElement"/> to <typeparamref name="TOutput"/>.
        /// </summary>
        /// <param name="source">Required. The data to convert.</param>
        /// <returns>An <see cref="IEnumerable{TOutput}"/> containing the converted results.</returns>
        public override IEnumerable<TOutput> Convert(XElement source) {
            if (string.IsNullOrEmpty(ElementName)) {
                throw new InvalidOperationException("ElementName must be set.");
            }

            var results = source.Descendants().Where(d => d.Name.LocalName == this.ElementName);

            return results.Select(ConvertSingle);
        }
    }
}