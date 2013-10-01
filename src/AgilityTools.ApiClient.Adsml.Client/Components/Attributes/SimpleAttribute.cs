using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// Represents an Adsml SimpleAttribute. Extends <see cref="AttributeBase"/>.
  /// </summary>
  public class SimpleAttribute : AttributeBase
  {
    public AttributeTypes AttributeType { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="attributeType">Reuired. The datatype of attribute.</param>
    public SimpleAttribute(AttributeTypes attributeType) : base("SimpleAttribute") {
      this.AttributeType = attributeType;
    }

    /// <summary>
    /// Factory method to create a new instance of <see cref="SimpleAttribute"/> with the provided parameters set.
    /// </summary>
    /// <param name="attributeType">The datatype of attribute.</param>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    /// <returns><see cref="SimpleAttribute"/></returns>
    public static SimpleAttribute New(AttributeTypes attributeType, string attributeName, string value = null) {
      var simpleAttribute = new SimpleAttribute(attributeType) {Name = attributeName};

      if (value != null) {
        simpleAttribute.Values.Add(value);
      }

      return simpleAttribute;
    }

    /// <summary>
    /// Factory method to create a new instance of <see cref="SimpleAttribute"/> with the provided parameters set.
    /// </summary>
    /// <param name="attributeType">The datatype of attribute.</param>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="values">The values of the attribute.</param>
    /// <returns><see cref="SimpleAttribute"/></returns>
    public static SimpleAttribute New(AttributeTypes attributeType, string attributeName, IEnumerable<string> values) {
      var simpleAttribute = new SimpleAttribute(attributeType) {Name = attributeName};

      var tmp = values.ToList();

      if (tmp.Any()) {
        simpleAttribute.Values.AddRange(tmp);
      }

      return simpleAttribute;
    }

    /// <summary>
    /// Converts the attribute to Admsl.
    /// </summary>
    /// <returns>An <see cref="XElement"/> containing the adsml representation of the attribute.</returns>
    public override XElement ToAdsml() {
      base.AttributeExtensions = new List<XAttribute> {
        new XAttribute("type", this.AttributeType.GetStringValue())
      };

      return base.ToAdsml();
    }
  }
}