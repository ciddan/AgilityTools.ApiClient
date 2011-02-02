using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class StructureValue : IAdsmlSerializable<XElement>
    {
        public int LanguageId { get; set; }
        public string Scope { get; set; }
        public string Value { get; set; }

        public StructureValue() {
            
        }

        public StructureValue(int languageId, string value, string scope = "global") {
            this.LanguageId = languageId;
            this.Value = value;
            this.Scope = scope;
        }

        public XElement ToAdsml() {
            if (string.IsNullOrEmpty(this.Scope))
                this.Scope = "global";

            this.Validate();

            return new XElement("StructureValue", new XAttribute("langId", this.LanguageId.ToString()),
                                new XAttribute("scope", this.Scope), 
                                new XCData(this.Value));
        }

        private void Validate() {
            if (this.LanguageId == 0)
                throw new ApiSerializationValidationException("LanguageId has to be set.");
	    }
    }
}