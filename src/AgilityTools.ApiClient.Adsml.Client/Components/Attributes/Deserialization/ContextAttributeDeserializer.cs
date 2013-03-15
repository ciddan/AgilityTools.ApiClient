using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Deserializes an <see cref="XElement"/> into a <see cref="ContextAttribute"/>. Implements <see cref="IAdsmlAttributeDeserializer"/>.
    /// </summary>
    public class ContextAttributeDeserializer : IAdsmlAttributeDeserializer
    {
        /// <summary>
        /// Deserializes the provided <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">Required. The <see cref="XElement"/> to deserialize.</param>
        /// <returns><see cref="ContextAttribute"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="element"/> is null.</exception>
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