using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents an Adsml RelationAttribute. Extends <see cref="AttributeBase"/>.
    /// </summary>
    public class RelationAttribute : AttributeBase
    {
        public int DefinitionId { get; set; }
        public string NameParserClass { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        internal RelationAttribute() : base("RelationAttribute") {
            this.AttributeExtensions = new List<XAttribute>();
        }

        /// <summary>
        /// Overload. Sets the provided parameters.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="definitionId">The definition id of the attribute.</param>
        /// <param name="nameParserClass"></param>
        public RelationAttribute(string name, int definitionId = -1, string nameParserClass = null)
            : base("RelationAttribute") {
            base.AttributeExtensions = new List<XAttribute>();

            base.Name = name;
            this.NameParserClass = nameParserClass;
            this.DefinitionId = definitionId;
        }

        /// <summary>
        /// Factory method for instantiating a new instance of a <see cref="RelationAttribute"/> with the provided parameters set.
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="definitionId">The definition id of the attribute.</param>
        /// <param name="nameParserClass">Optional.</param>
        /// <returns><see cref="RelationAttribute"/></returns>
        public static RelationAttribute New(string name, string value, int definitionId = -1,
                                            string nameParserClass = null) {
            var relationAttribute = new RelationAttribute(name, definitionId, nameParserClass);

            if (value != null) {
                relationAttribute.Values.Add(value);
            }

            return relationAttribute;
        }

        /// <summary>
        /// Factory method for instantiating a new instance of a <see cref="RelationAttribute"/> with the provided parameters set.
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="values">The values of the attribute.</param>
        /// <param name="definitionId">The definition id of the attribute.</param>
        /// <param name="nameParserClass">Optional.</param>
        /// <returns><see cref="RelationAttribute"/></returns>
        public static RelationAttribute New(string name, IEnumerable<string> values, int definitionId = -1,
                                            string nameParserClass = null) {
            var relationAttribute = new RelationAttribute(name, definitionId, nameParserClass);

            if (values != null && values.Count() >= 1) {
                relationAttribute.Values.AddRange(values);
            }

            return relationAttribute;
        }

        /// <summary>
        /// Converts the attribute to Admsl.
        /// </summary>
        /// <returns>An <see cref="XElement"/> containing the adsml representation of the attribute.</returns>
        public override XElement ToAdsml() {
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