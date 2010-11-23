using System.Collections.Generic;
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

        public static RelationAttribute New(string name, object value, int definitionId = -1, string nameParserClass = null) {
            return new RelationAttribute(name, definitionId, nameParserClass) { Value = value };
        }
    }
}