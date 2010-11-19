using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public abstract class AttributeBase : IAdsmlAttribute<XElement>
    {
        public string Name { get; set; }
        public object Value { get; set; }

        internal string ElementName { get; set; }
        internal IEnumerable<XAttribute> AttributeExtensions { get; set; }

        protected AttributeBase(string elementName) {
            this.ElementName = elementName;
        }

        public virtual XElement ToAdsml() {
            Validate();

            var element = new XElement(this.ElementName, new XAttribute("name", this.Name), new XElement("Value", this.Value));

            if (this.AttributeExtensions != null) {
                element.Add(AttributeExtensions);
            }

            return element;
        }

        protected virtual void Validate() {
            if (string.IsNullOrEmpty(this.Name)) {
                throw new ApiSerializationValidationException("Name must be set.");
            }

            if (this.Value == null) {
                throw new ApiSerializationValidationException("Value must be set.");
            }
        }
    }
}