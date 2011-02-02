using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class ContextAttribute : AttributeBase
    {
        public string NameParserClass { get; set; }

        public ContextAttribute(string nameParserClass = null) : base("ContextAttribute") {
            this.NameParserClass = nameParserClass;
        }

        public static ContextAttribute New(string name, string value, string nameParserClass = null) {
            var contextAttribute = new ContextAttribute(nameParserClass) {Name = name};

            contextAttribute.Values.Add(value);

            return contextAttribute;
        }

        public static ContextAttribute New(string name, IEnumerable<string> values, string nameParserClass = null) {
            var contextAttribute = new ContextAttribute(nameParserClass) { Name = name };

            if (values != null) {
                contextAttribute.Values.AddRange(values);
            }

            return contextAttribute;
        }

        public override XElement ToAdsml()
        {
            if (!string.IsNullOrEmpty(NameParserClass)) {
                base.AttributeExtensions = new List<XAttribute> {
                                               new XAttribute("nameParserClass", this.NameParserClass)
                                           };
            }

            return base.ToAdsml();
        }
    }
}