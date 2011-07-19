using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Holds a value for a <see cref="CompositeAttribute"/>.
    /// </summary>
    public class Field : IAdsmlSerializable<XElement>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// Converts the object to Admsl.
        /// </summary>
        /// <returns><see cref="XElement"/></returns>
        /// <exception cref="InvalidOperationException">Thrown if either Name or Type is unset.</exception>
        public XElement ToAdsml() {
            if (string.IsNullOrEmpty(this.Name)) {
                throw new System.InvalidOperationException("Name not set.");
            }

            if (string.IsNullOrEmpty(this.Type)) {
                throw new System.InvalidOperationException("Type not set.");
            }

            return
                new XElement("Field",
                             new XAttribute("name", this.Name),
                             new XAttribute("type", this.Type),
                             new XCData(this.Value));
        }
    }
}