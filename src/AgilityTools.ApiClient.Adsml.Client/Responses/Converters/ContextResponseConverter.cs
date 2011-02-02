using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ContextResponseConverter : IResponseConverter<XElement, ContextResponse>
    {
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

        public IEnumerable<ContextResponse> Convert(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            CheckResponse(source);

            var contexts = source.Descendants()
                .Where(d => (d.Name.LocalName == "Context" || d.Name.LocalName == "StructureContext"));

            return contexts.Select(ConvertSingle);
        }

        private static void CheckResponse(XElement source) {
            source.ValidateAdsmlResponse();

            if ((source.Descendants("StructureContext").Count() < 1) && (source.Descendants("Context").Count() < 1)) {
                throw new InvalidOperationException("Not a valid ContextResponse.");
            }
        }
    }
}