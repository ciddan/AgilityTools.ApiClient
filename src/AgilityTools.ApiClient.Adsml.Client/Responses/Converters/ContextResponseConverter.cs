using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ContextResponseConverter : IResponseConverter<XElement, ContextResponse>
    {
        /// <summary>
        /// Converts the input of <see cref="XElement"/> to a single <see cref="ContextResponse"/>.
        /// </summary>
        /// <param name="source">Required. The <see cref="XElement"/> to convert.</param>
        /// <returns>A <see cref="ContextResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        private static ContextResponse ConvertSingle(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            return new ContextResponse
                   {
                       Name = (string) source.Attribute("name"),
                       IdPath = (string) source.Attribute("idPath"),
                       SortPath = (string) source.Attribute("sortPath"),
                       Attributes = new List<IAdsmlAttribute>(
                           source.Descendants().Where(d => d.Name.LocalName.Contains("Attribute"))
                               .Select(AttributeDeserializer.Deserialize)
                           )
                   };
        }

        /// <summary>
        /// Converts the input of <see cref="XElement"/> to an <see cref="IEnumerable{T}"/> of <see cref="ContextResponse"/>.
        /// </summary>
        /// <param name="source">Required. The data to convert.</param>
        /// <returns>An <see cref="IEnumerable{TOutput}"/> of <see cref="ContextResponse"/> containing the converted results.</returns>
        public IEnumerable<ContextResponse> Convert(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            CheckResponse(source);

            var contexts = source.Descendants()
                .Where(d => (d.Name.LocalName == "Context" || d.Name.LocalName == "StructureContext"))
                .ToList();

            return contexts.Count() == 0 
                ? new List<ContextResponse>() 
                : contexts.Select(ConvertSingle);
        }

        /// <summary>
        /// Checks to see whether the response really is a ContextResponse.
        /// </summary>
        /// <param name="source">The reponse to check for syntax validity.</param>
        private static void CheckResponse(XElement source) {
            source.ValidateAdsmlResponse();

            if (source.Descendants("SearchResults").Count() != 0) {
                
            } else {
                if ((source.Descendants("StructureContext").Count() < 1) && (source.Descendants("Context").Count() < 1)) {
                    throw new InvalidOperationException(string.Format("Not a valid ContextResponse.\r\nResponse:\r\n{0}", source));
                }    
            }
        }
    }
}