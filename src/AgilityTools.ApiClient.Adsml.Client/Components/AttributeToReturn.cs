using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class AttributeToReturn : IApiSerializable
    {
        public int DefinitionId { get; set; }
        public string Name { get; set; }

        public XElement ToApiXml() {
            this.Validate();

            var attributeElement = new XElement("Attribute");

            if (!string.IsNullOrEmpty(this.Name))
                attributeElement.Add(new XAttribute("name", this.Name));

            if(this.DefinitionId != 0)
                attributeElement.Add(new XAttribute("id", this.DefinitionId.ToString()));

            return attributeElement;
        }

        public void Validate() {
            if (DefinitionId == 0 && string.IsNullOrEmpty(Name))
                throw new ApiSerializationValidationException(
                    "Invalid settings. Either DefinitionId or Name must be set.");
        }
    }
}