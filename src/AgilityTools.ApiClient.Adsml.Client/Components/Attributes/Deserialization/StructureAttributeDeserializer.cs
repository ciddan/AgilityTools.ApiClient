using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization
{
    public class StructureAttributeDeserializer : IAdsmlAttributeDeserializer
    {
        public IAdsmlAttribute Deserialize(XElement element) {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            if ((string.IsNullOrEmpty(element.Name.ToString()) || element.Name.ToString() != "StructureAttribute")) {
                throw new InvalidOperationException("Not a valid StructureAttribute.");
            }

            int definitionId = 0;
            int.TryParse((string) element.Attribute("id"), out definitionId);

            var structureAttribute = new StructureAttribute
                                     {
                                         DefinitionId = definitionId,
                                         Name = (string) element.Attribute("name"),
                                         Values = new List<StructureValue>(
                                             element.Descendants("StructureValue")
                                                 .Select(sv => new StructureValue
                                                               {
                                                                   LanguageId = int.Parse((string) sv.Attribute("langId")),
                                                                   Scope = (string) sv.Attribute("scope"),
                                                                   Value = sv.Value
                                                               })
                                             )
                                     };

            return structureAttribute;
        }
    }
}