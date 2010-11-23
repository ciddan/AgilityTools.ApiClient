using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class ContextAttribute : AttributeBase
    {
        public ContextAttribute(string nameParserClass = null) : base("ContextAttribute") {
            if (!string.IsNullOrEmpty(nameParserClass)) {
                base.AttributeExtensions = new List<XAttribute>
                                           {
                                               new XAttribute("nameParserClass", nameParserClass)
                                           };
            }
        }

        public static ContextAttribute New(string name, object value, string nameParserClass = null) {
            return new ContextAttribute(nameParserClass) {Name = name, Value = value};
        }
    }
}