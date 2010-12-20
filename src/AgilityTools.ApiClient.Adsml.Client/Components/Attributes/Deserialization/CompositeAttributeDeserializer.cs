using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization
{
    public class CompositeAttributeDeserializer : IAdsmlAttributeDeserializer
    {
        public IAdsmlAttribute Deserialize(XElement attribute) {
            if (attribute == null) {
                throw new ArgumentNullException("attribute");
            }

            if ((string.IsNullOrEmpty(attribute.Name.ToString()) || attribute.Name.ToString() != "CompositeAttribute")) {
                throw new InvalidOperationException("Not a valid CompositeAttribute.");
            }

            return new CompositeAttribute
                   {
                       Name = attribute.Name.ToString(),
                       Fields = new List<CompositeValue>
                           (
                           attribute.Descendants("Field")
                               .Select(f => new CompositeValue
                                            {
                                                Name = (string) f.Attribute("name"),
                                                Type = (string) f.Attribute("type"),
                                                Value = f.Value
                                            })
                           )
                   };
        }
    }
}