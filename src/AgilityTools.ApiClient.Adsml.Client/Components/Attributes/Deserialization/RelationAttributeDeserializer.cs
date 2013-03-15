using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Deserializes an <see cref="XElement"/> into a <see cref="RelationAttribute"/>. Implements <see cref="IAdsmlAttributeDeserializer"/>.
    /// </summary>
    public class RelationAttributeDeserializer : IAdsmlAttributeDeserializer
    {
        /// <summary>
        /// Deserializes the provided <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">Required. The <see cref="XElement"/> to deserialize.</param>
        /// <returns><see cref="RelationAttribute"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="element"/> is null.</exception>
        public IAdsmlAttribute Deserialize(XElement element) {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            if ((string.IsNullOrEmpty(element.Name.ToString()) || element.Name.ToString() != "RelationAttribute")) {
                throw new InvalidOperationException("Not a valid RelationAttribute.");
            }

            var relationAttribute = new RelationAttribute
                                    {
                                        Name = (string) element.Attribute("name"),
                                        Values = new List<string>(
                                                element.Descendants("Value").Select(d => d.Value)
                                            )
                                    };

            if (!string.IsNullOrEmpty((string) element.Attribute("nameParserClass"))) {
                relationAttribute.NameParserClass = (string) element.Attribute("nameParserClass");
            }

            if (!string.IsNullOrEmpty((string) element.Attribute("id"))) {
                relationAttribute.DefinitionId = int.Parse((string) element.Attribute("id"));
            }

            return relationAttribute;
        }
    }
}