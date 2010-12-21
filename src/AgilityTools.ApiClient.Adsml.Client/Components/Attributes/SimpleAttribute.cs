using System.Linq;
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

        public static SimpleAttribute New(AttributeTypes attributeType, string attributeName, string value = null) {
            var simpleAttribute = new SimpleAttribute(attributeType) { Name = attributeName };

            if (value != null) {
                simpleAttribute.Values.Add(value);
            }

            return simpleAttribute;
        }

        public static SimpleAttribute New(AttributeTypes attributeType, string attributeName, IEnumerable<string> values) {
            var simpleAttribute = new SimpleAttribute(attributeType) { Name = attributeName };

            if (values != null && values.Count() >= 1) {
                simpleAttribute.Values.AddRange(values);
            }

            return simpleAttribute;
        }
    }
}