using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// A class defining an attribute in an AttributeToReturn context.
  /// </summary>
  public class AttributeToReturn : IAttributeControl
  {
    public string Name { get; set; }

    internal AttributeToReturn() {
    }

    /// <summary>
    /// Serializes the object to ADSML form.
    /// </summary>
    /// <returns>An <see cref="XElement"/>.</returns>
    public XElement ToAdsml() {
      this.Validate();

      var attributeElement = new XElement("Attribute");

      if (!string.IsNullOrEmpty(this.Name))
        attributeElement.Add(new XAttribute("name", this.Name));

      return attributeElement;
    }

    /// <summary>
    /// A factory method that returns a new instance of <see cref="AttributeToReturn"/> with the supplied name.
    /// </summary>
    /// <param name="name">The name of the attribute to return.</param>
    /// <returns><see cref="AttributeToReturn"/></returns>
    public static AttributeToReturn WithName(string name) {
      return new AttributeToReturn {Name = name};
    }

    /// <summary>
    /// Validates the state of the object.
    /// </summary>
    /// <exception cref="ApiSerializationValidationException">Thrown if the name and definition id are not set. The Api requires at least on to be set.</exception>
    public void Validate() {
      if (string.IsNullOrEmpty(Name))
        throw new ApiSerializationValidationException(
          "Invalid settings. Name must be set."
        );
    }
  }
}