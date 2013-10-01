using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// Abstract base class for some implementations of <see cref="IAdsmlAttribute"/>. Implements <see cref="IAdsmlAttribute"/>.
  /// </summary>
  public abstract class AttributeBase : IAdsmlAttribute
  {
    public string Name { get; set; }
    public List<string> Values { get; set; }

    internal string ElementName { get; set; }
    protected IList<XAttribute> AttributeExtensions { get; set; }

    /// <summary>
    /// Constructor. Can only be called by implementations.
    /// </summary>
    /// <param name="elementName">Sets the name of the xml element.</param>
    protected AttributeBase(string elementName) {
      this.ElementName = elementName;
      this.Values = new List<string>();
    }

    /// <summary>
    /// Returns the name of the attribute.
    /// </summary>
    /// <returns></returns>
    public string GetName() {
      return this.Name;
    }

    /// <summary>
    /// Gets all values contained within the attribute.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="AttributeValue"/></returns>
    public IEnumerable<AttributeValue> GetValues() {
      return this.Values.Select(v => new AttributeValue {Value = v, LanguageId = 0});
    }

    public bool HasValues() {
      return this.Values.Count > 0;
    }

    /// <summary>
    /// Converts the attribute to Admsl.
    /// </summary>
    /// <returns>An <see cref="XElement"/> containing the adsml representation of the attribute.</returns>
    public virtual XElement ToAdsml() {
      Validate();

      var element = new XElement(this.ElementName, new XAttribute("name", this.Name));

      if (this.Values.Any()) {
        foreach (var value in Values) {
          element.Add(new XElement("Value", new XCData(value)));    
        }
      }

      if (this.AttributeExtensions != null && this.AttributeExtensions.Any()) {
        element.Add(AttributeExtensions);
      }

      return element;
    }

    /// <summary>
    /// Validates the attribute.
    /// </summary>
    /// <exception cref="ApiSerializationValidationException">Thrown if the validation fails.</exception>
    protected virtual void Validate() {
      if (string.IsNullOrEmpty(this.Name)) {
        throw new ApiSerializationValidationException("Name must be set.");
      }
    }
  }
}