using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class RelationAttribute : AttributeBase
    {
        public RelationAttribute(string name, int definitionId = -1, string nameParserClass = null) : base("RelationAttribute") {
            base.AttributeExtensions = new List<XAttribute>();
            base.Name = name;

            if (!string.IsNullOrEmpty(nameParserClass)) {
                base.AttributeExtensions.Add(new XAttribute("nameParserClass", nameParserClass));
            }

            if (definitionId != -1) {
                base.AttributeExtensions.Add(new XAttribute("id", definitionId));
            }
        }

        public static RelationAttribute New(string name, string value, int definitionId = -1, string nameParserClass = null) {
            var relationAttribute = new RelationAttribute(name, definitionId, nameParserClass);

            if (value != null) {
                relationAttribute.Values.Add(value);
            }

            return relationAttribute;
        }

        public static RelationAttribute New(string name, IEnumerable<string> values, int definitionId = -1, string nameParserClass = null) {
            var relationAttribute = new RelationAttribute(name, definitionId, nameParserClass);

            if (values != null && values.Count() >= 1) {
                relationAttribute.Values.AddRange(values);
            }

            return relationAttribute;
        }
    }
}