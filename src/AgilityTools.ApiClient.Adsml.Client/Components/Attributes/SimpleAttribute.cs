using System.Xml.Linq;
using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class SimpleAttribute : AttributeBase
    {
        public SimpleAttribute(AttributeTypes attributeType) : base("SimpleAttribute") {
            base.AttributeExtensions = new List<XAttribute>
                                        {
                                            new XAttribute("type", attributeType.GetStringValue())
                                        };
        }

        public static SimpleAttribute New(AttributeTypes attributeType, string attributeName, object value) {
            return new SimpleAttribute(attributeType) {Name = attributeName, Value = value};
        }
    }
}