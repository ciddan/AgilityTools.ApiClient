using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class ContextAttributeDeserializer : IAdsmlAttributeDeserializer
    {
        public IAdsmlAttribute Deserialize(XElement element) {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            if ((string.IsNullOrEmpty(element.Name.ToString()) || element.Name.ToString() != "ContextAttribute")) {
                throw new InvalidOperationException("Not a valid ContextAttribute.");
            }

            var contextAttribute = new ContextAttribute
                                   {
                                       Name = (string) element.Attribute("name"),
                                       Values = new List<string>(
                                           element.Descendants("Value").Select(d => d.Value)
                                           )
                                   };

            if (!string.IsNullOrEmpty((string) element.Attribute("nameParserClass"))) {
                contextAttribute.NameParserClass = (string)element.Attribute("nameParserClass");
            }

            return contextAttribute;
        }
    }
}