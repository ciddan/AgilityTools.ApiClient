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

        public static ContextAttribute New(string name, string value, string nameParserClass = null) {
            var contextAttribute = new ContextAttribute(nameParserClass) {Name = name};

            contextAttribute.Values.Add(value);

            return contextAttribute;
        }

        public static ContextAttribute New(string name, string nameParserClass, IEnumerable<string> values) {
            var contextAttribute = new ContextAttribute(nameParserClass) { Name = name };

            if (values != null) {
                contextAttribute.Values.AddRange(values);
            }

            return contextAttribute;
        }
    }
}