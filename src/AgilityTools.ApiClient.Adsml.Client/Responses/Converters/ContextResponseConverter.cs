using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
  public class ContextResponseConverter : ResponseConverter<ContextResponse>
  {
    public ContextResponseConverter(string validationDocument) : base(validationDocument, new[] { "SearchResponse", "ModifyResponse", "LookupResponse" }) { }
    
    /// <summary>
    /// Converts the input of <see cref="XElement"/> to a single <see cref="ContextResponse"/>.
    /// </summary>
    /// <param name="source">Required. The <see cref="XElement"/> to convert.</param>
    /// <returns>A <see cref="ContextResponse"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
    protected override ContextResponse ConvertSingle(XElement source) {
      if (source == null) {
        throw new ArgumentNullException("source");
      }
      
      return new ContextResponse {
        Name = (string)source.Attribute("name"),
        IdPath = (string)source.Attribute("idPath"),
        SortPath = (string)source.Attribute("sortPath"),
        Attributes = new List<IAdsmlAttribute>(
          source.Descendants().Where(d => d.Name.LocalName.Contains("Attribute"))
          .Select(AttributeDeserializer.Deserialize)
          )
      };
    }
    
    /// <summary>
    /// Converts the input of <see cref="XElement"/> to an <see cref="IEnumerable{T}"/> of <see cref="ContextResponse"/>.
    /// </summary>
    /// <param name="source">Required. The data to convert.</param>
    /// <returns>An <see cref="IEnumerable{TOutput}"/> of <see cref="ContextResponse"/> containing the converted results.</returns>
    public override IEnumerable<ContextResponse> Convert(XElement source) {
      if (source == null) {
        throw new ArgumentNullException("source");
      }
      
      CheckResponse(source);
      
      var contexts = source.Descendants()
        .Where(d => (d.Name.LocalName == "Context" || d.Name.LocalName == "StructureContext"))
          .ToList();
      
      return !contexts.Any()
        ? new List<ContextResponse>()
          : contexts.Select(ConvertSingle);
    }
  }
}