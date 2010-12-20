using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes
{
    public class CompositeValue : IAdsmlSerializable<XElement>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

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