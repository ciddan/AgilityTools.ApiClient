using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Deserializes an <see cref="XElement"/> into a <see cref="SimpleAttribute"/>. Implements <see cref="IAdsmlAttributeDeserializer"/>.
    /// </summary>
    public class SimpleAttributeDeserializer : IAdsmlAttributeDeserializer
    {
        /// <summary>
        /// Deserializes the provided <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">Required. The <see cref="XElement"/> to deserialize.</param>
        /// <returns><see cref="SimpleAttribute"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="element"/> is null.</exception>
        public IAdsmlAttribute Deserialize(XElement element) {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            if ((string.IsNullOrEmpty(element.Name.ToString()) || element.Name.ToString() != "SimpleAttribute")) {
                throw new InvalidOperationException("Not a valid SimpleAttribute.");
            }

            var type = (string) element.Attribute("type");
            type = type.Capitalize();

            var simpleAttribute = new SimpleAttribute((AttributeTypes) Enum.Parse(typeof(AttributeTypes), type))
                                    {
                                        Name = (string) element.Attribute("name"),
                                        Values = new List<string>(
                                            element.Descendants("Value").Select(d => d.Value)
                                            )
                                    };

            return simpleAttribute;
        }
    }
}