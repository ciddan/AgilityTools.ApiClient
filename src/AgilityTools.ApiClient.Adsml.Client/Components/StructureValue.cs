using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class StructureValue : IApiSerializable
    {
        public int LanguageId { get; set; }
        public string Scope { get; set; }
        public string Value { get; set; }

        public XElement ToApiXml() {
            if (string.IsNullOrEmpty(this.Scope))
                this.Scope = "global";

            this.Validate();

            return new XElement("StructureValue", new XAttribute("langId", this.LanguageId.ToString()), new XAttribute("scope", this.Scope), this.Value);
        }

        public void Validate() {
            if (this.LanguageId == 0)
                throw new ApiSerializationValidationException("LanguageId has to be set.");
	    }
    }
}