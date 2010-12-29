using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Responses.Converters
{
    public class ContextResponseConverter : IResponseConverter<XElement, ContextResponse>
    {
        private static ContextResponse ConvertSingle(XElement source) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            CheckResponse(source);

            XElement context = source.Descendants()
                .Where(d => (d.Name.LocalName == "Context" || d.Name.LocalName == "StructureContext")).SingleOrDefault();

            if (context == null) {
                return null;   
            }

            return new ContextResponse
                   {
                       Name = (string) context.Attribute("name"),
                       IdPath = (string) context.Attribute("idPath"),
                       SortPath = (string) context.Attribute("sortPath"),
                       Attributes = new List<IAdsmlAttribute>(
                           context.Descendants().Where(d => d.Name.LocalName.Contains("Attribute"))
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