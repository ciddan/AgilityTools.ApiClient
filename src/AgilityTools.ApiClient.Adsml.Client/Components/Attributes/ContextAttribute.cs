using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents an Adsml ContextAttribute. Extends <see cref="AttributeBase"/>.
    /// </summary>
    public class ContextAttribute : AttributeBase
    {
        public string NameParserClass { get; set; }

        public ContextAttribute(string nameParserClass = null) : base("ContextAttribute") {
            this.NameParserClass = nameParserClass;
        }

        /// <summary>
        /// Factory method for instantiating a new instance of a <see cref="ContextAttribute"/> with the provided parameters set.
        /// </summary>
        /// <param name="name">Optional. The name of the attribute.</param>
        /// <param name="value">Optional. The value of the attribute.</param>
        /// <param name="nameParserClass">Optional.</param>
        /// <returns><see cref="ContextAttribute"/></returns>
        public static ContextAttribute New(string name, string value, string nameParserClass = null) {
            var contextAttribute = new ContextAttribute(nameParserClass) {Name = name};

            contextAttribute.Values.Add(value);

            return contextAttribute;
        }

        /// <summary>
        /// Factory method for instantiating a new instance of a <see cref="ContextAttribute"/> with the provided parameters set.
        /// </summary>
        /// <param name="name">Optional. The name of the attribute.</param>
        /// <param name="values">Optional. The values of the attribute.</param>
        /// <param name="nameParserClass">Optional.</param>
        /// <returns><see cref="ContextAttribute"/></returns>
        public static ContextAttribute New(string name, IEnumerable<string> values, string nameParserClass = null) {
            var contextAttribute = new ContextAttribute(nameParserClass) { Name = name };

            if (values != null) {
                contextAttribute.Values.AddRange(values);
            }

            return contextAttribute;
        }

        /// <summary>
        /// Converts the attribute to Admsl.
        /// </summary>
        /// <returns>An <see cref="XElement"/> containing the adsml representation of the attribute.</returns>
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