using System.Xml.Linq;
using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class SimpleAttribute : AttributeBase
    {
        public SimpleAttribute(AttributeType attributeType) : base("SimpleAttribute") {
            base.AttributeExtensions = new List<XAttribute>
                                        {
                                            new XAttribute("type", attributeType.GetStringValue())
                                        };
        }

        public static SimpleAttribute New(AttributeType attributeType, string attributeName, object value) {
            return new SimpleAttribute(attributeType) {Name = attributeName, Value = value};
        }
    }

    public enum AttributeType
    {
        [StringValue("text")]
        Text = 1,
        [StringValue("integer")]
        Integer = 2,
        [StringValue("decimal")]
        Decimal = 3,
        [StringValue("date")]
        Date = 4,
        [StringValue("binary")]
        Binary = 5
    }
}