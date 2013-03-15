using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Deserializes an <see cref="XElement"/> into a <see cref="CompositeAttribute"/>. Implements <see cref="IAdsmlAttributeDeserializer"/>.
    /// </summary>
    public class CompositeAttributeDeserializer : IAdsmlAttributeDeserializer
    {
        /// <summary>
        /// Deserializes the provided <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">Required. The <see cref="XElement"/> to deserialize.</param>
        /// <returns><see cref="CompositeAttribute"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="element"/> is null.</exception>
        public IAdsmlAttribute Deserialize(XElement element) {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            if ((string.IsNullOrEmpty(element.Name.ToString()) || element.Name.ToString() != "CompositeAttribute")) {
                throw new InvalidOperationException("Not a valid CompositeAttribute.");
            }

            return new CompositeAttribute
                   {
                       Name = (string) element.Attribute("name"),
                       CompositeValues = new List<CompositeValue>
                           (
                           element.Descendants("CompositeValue")
                                    .Select(cv => new CompositeValue
                                                  {
                                                      Fields = new List<Field>(
                                                          cv.Descendants("Field")
                                                            .Select(f => new Field
                                                                         {
                                                                             Name = (string) f.Attribute("name"),
                                                                             Type = (string) f.Attribute("type"),
                                                                             Value = f.Value
                                                                         })
                                                          )
                                                  })
                           )
                   };
        }
    }
}