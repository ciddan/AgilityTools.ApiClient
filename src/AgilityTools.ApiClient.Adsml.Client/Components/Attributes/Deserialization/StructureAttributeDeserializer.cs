using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// Deserializes an <see cref="XElement"/> into a <see cref="StructureAttribute"/>. Implements <see cref="IAdsmlAttributeDeserializer"/>.
  /// </summary>
  public class StructureAttributeDeserializer : IAdsmlAttributeDeserializer
  {
    /// <summary>
    /// Deserializes the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">Required. The <see cref="XElement"/> to deserialize.</param>
    /// <returns><see cref="StructureAttribute"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="element"/> is null.</exception>
    public IAdsmlAttribute Deserialize(XElement element) {
      if (element == null) {
        throw new ArgumentNullException("element");
      }
      
      if ((string.IsNullOrEmpty(element.Name.ToString()) || element.Name.ToString() != "StructureAttribute")) {
        throw new InvalidOperationException("Not a valid StructureAttribute.");
      }
      
      int definitionId;
      int.TryParse((string) element.Attribute("id"), out definitionId);
      
      var structureAttribute = new StructureAttribute {
        DefinitionId = definitionId,
        Name = (string) element.Attribute("name"),
        Values = new List<StructureValue> (
          element.Descendants("StructureValue")
          .Select(sv => new StructureValue {
            LanguageId = int.Parse((string) sv.Attribute("langId")),
            Scope = (Scopes) Enum.Parse(typeof(Scopes), (string)sv.Attribute("scope"), true),
            Value = sv.Value,
            StateId = ((string) sv.Attribute("stateId")).ToNullableInt()
          })
        )
      };
      
      return structureAttribute;
    }
  }
}