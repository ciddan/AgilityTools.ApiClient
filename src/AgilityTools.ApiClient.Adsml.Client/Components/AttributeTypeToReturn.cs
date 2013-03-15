using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  public class AttributeTypeToReturn : IAttributeControl
  {
    public AttributeDataType Type { get; set; }

    public string OuterNodeName {
      get { return "AttributeTypesToReturn"; }
    }

    /// <summary>
    /// Constructor. Internal.
    /// </summary>
    internal AttributeTypeToReturn() {
    }

    public static AttributeTypeToReturn OfType(AttributeDataType type) {
      return new AttributeTypeToReturn {Type = type};
    }

    /// <summary>
    /// Serializes the object to ADSML xml form.
    /// </summary>
    /// <returns><see cref="XElement"/></returns>
    public XElement ToAdsml() {
      var attributeElement = new XElement("AttributeType");

      attributeElement.Add(new XAttribute("name", this.Type.GetStringValue()));

      return attributeElement;
    }
  }

  /// <summary>
  /// Enum containing the various Agility attribute data types. The string value is the adsml.xsd mandated name for the attribute type.
  /// </summary>
  public enum AttributeDataType
  {
    [StringValue("structure")] Structure,
    [StringValue("context")] Context,
    [StringValue("relation")] Relation,
    [StringValue("composite")] Composite,
    [StringValue("simple-text")] SimpleText,
    [StringValue("simple-integer")] SimpleInteger,
    [StringValue("simple-decimal")] SimpleDecimal,
    [StringValue("simple-date")] SimpleDate,
    [StringValue("simple-binary")] SimpleBinary,
    [StringValue("simple-boolean")] SimpleBoolean,
    [StringValue("structure-text")] StructureText,
    [StringValue("structure-number")] StructureNumber,
    [StringValue("structure-date")] StructureDate,
    [StringValue("structure-simple")] StructureSimple,
    [StringValue("structure-xml")] StructureXml,
    [StringValue("structure-image")] StructureImage,
    [StringValue("structure-binary")] StructureBinary,
    [StringValue("structure-table")] StructureTable,
    [StringValue("structure-whiteboard")] StructureWhiteboard,
    [StringValue("structure-path")] StructurePath,
    [StringValue("structure-script")] StructureScript,
    [StringValue("relation-asset")] RelationAsset,
    [StringValue("relation-index")] RelationIndex,
    [StringValue("relation-vendor")] RelationVendor,
    [StringValue("relation-product")] RelationProduct,
    [StringValue("relation-design")] RelationDesign
  }
}