using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class RelationAttribute : AttributeBase
    {
        public int DefinitionId { get; set; }
        public string NameParserClass { get; set; }

        internal RelationAttribute() : base("RelationAttribute") {
            this.AttributeExtensions = new List<XAttribute>();
        }

        public RelationAttribute(string name, int definitionId = -1, string nameParserClass = null) : base("RelationAttribute") {
            base.AttributeExtensions = new List<XAttribute>();

            base.Name = name;
            this.NameParserClass = nameParserClass;
            this.DefinitionId = definitionId;
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

        public override XElement ToAdsml()
        {
            if (!string.IsNullOrEmpty(this.NameParserClass)) {
                base.AttributeExtensions.Add(new XAttribute("nameParserClass", this.NameParserClass));
            }

            if (this.DefinitionId != -1) {
                base.AttributeExtensions.Add(new XAttribute("id", this.DefinitionId));
            }

            return base.ToAdsml();
        }
    }
}