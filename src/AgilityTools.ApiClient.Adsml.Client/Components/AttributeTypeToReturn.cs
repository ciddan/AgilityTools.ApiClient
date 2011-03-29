using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class AttributeTypeToReturn : IAttributeControl
    {
        public AttributeDataType Type { get; set; }

        internal AttributeTypeToReturn() { }

        public static AttributeTypeToReturn OfType(AttributeDataType type) {
            return new AttributeTypeToReturn {Type = type};
        }

        public XElement ToAdsml() {
            var attributeElement = new XElement("AttributeType");

            attributeElement.Add(new XAttribute("name", this.Type.GetStringValue()));

            return attributeElement;
        }
    }

    public enum AttributeDataType
    {
        [StringValue("structure")]
        Structure,
        [StringValue("context")]
        Context,
        [StringValue("relation")]
        Relation,
        [StringValue("composite")]
        Composite,
        [StringValue("simple-text")]
        SimpleText,
        [StringValue("simple-integer")]
        SimpleInteger,
        [StringValue("simple-decimal")]
        SimpleDecimal,
        [StringValue("simple-date")]
        SimpleDate,
        [StringValue("simple-binary")]
        SimpleBinary,
        [StringValue("simple-boolean")]
        SimpleBoolean,
        [StringValue("structure-text")]
        StructureText,
        [StringValue("structure-number")]
        StructureNumber,
        [StringValue("structure-date")]
        StructureDate,
        [StringValue("structure-simple")]
        StructureSimple,
        [StringValue("structure-xml")]
        StructureXml,
        [StringValue("structure-image")]
        StructureImage,
        [StringValue("structure-binary")]
        StructureBinary,
        [StringValue("structure-table")]
        StructureTable,
        [StringValue("structure-whiteboard")]
        StructureWhiteboard,
        [StringValue("structure-path")]
        StructurePath,
        [StringValue("structure-script")]
        StructureScript,
        [StringValue("relation-asset")]
        RelationAsset,
        [StringValue("relation-index")]
        RelationIndex,
        [StringValue("relation-vendor")]
        RelationVendor,
        [StringValue("relation-product")]
        RelationProduct,
        [StringValue("relation-design")]
        RelationDesign
    }
}