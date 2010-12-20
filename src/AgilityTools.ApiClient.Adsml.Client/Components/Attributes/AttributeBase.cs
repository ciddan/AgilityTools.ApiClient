using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public abstract class AttributeBase : IAdsmlAttribute
    {
        public string Name { get; set; }
        public object Value { get; set; }

        internal string ElementName { get; set; }
        internal IList<XAttribute> AttributeExtensions { get; set; }

        protected AttributeBase(string elementName) {
            this.ElementName = elementName;
        }

        public virtual XElement ToAdsml() {
            Validate();

            var element = new XElement(this.ElementName, new XAttribute("name", this.Name));

            if (this.Value != null) {
                element.Add(new XElement("Value", new XCData(this.Value.ToString())));
            }

            if (this.AttributeExtensions != null && this.AttributeExtensions.Count() >= 1) {
                element.Add(AttributeExtensions);
            }

            return element;
        }

        protected virtual void Validate() {
            if (string.IsNullOrEmpty(this.Name)) {
                throw new ApiSerializationValidationException("Name must be set.");
            }
        }
    }
}