using System.Xml.Linq;
using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class SimpleAttribute : AdsmlAttribute
    {
        public SimpleAttribute(SimpleAttributeType attributeType) : base("SimpleAttribute") {
            base.AttributeExtensions = new List<XAttribute>
                                        {
                                            new XAttribute("simpleAttributeType", attributeType.GetStringValue())
                                        };
        }
    }

    public enum SimpleAttributeType
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