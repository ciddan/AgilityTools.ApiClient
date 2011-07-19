using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Holds a value for a <see cref="StructureAttribute"/>.
    /// </summary>
    public class StructureValue : IAdsmlSerializable<XElement>
    {
        public int LanguageId { get; set; }
        public Scopes Scope { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public StructureValue() {
        }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="languageId">The language id of the language which the value is in.</param>
        /// <param name="value">The value.</param>
        /// <param name="scope">The <see cref="Scopes"/> scope of the attribute value. Defaults to <see cref="Scopes.Global"/>.</param>
        public StructureValue(int languageId, string value, Scopes scope = Scopes.Global) {
            this.LanguageId = languageId;
            this.Value = value;
            this.Scope = scope;
        }

        /// <summary>
        /// Converts the attribute to Admsl.
        /// </summary>
        /// <returns>An <see cref="XElement"/> containing the adsml representation of the attribute.</returns>
        public XElement ToAdsml() {
            this.Validate();

            return new XElement("StructureValue", new XAttribute("langId", this.LanguageId.ToString()),
                                new XAttribute("scope", this.Scope.GetStringValue()), 
                                new XCData(this.Value));
        }

        /// <summary>
        /// Validates the attribute.
        /// </summary>
        /// <exception cref="ApiSerializationValidationException">Thrown if the validation fails.</exception>
        private void Validate() {
            if (this.LanguageId == 0)
                throw new ApiSerializationValidationException("LanguageId has to be set.");
	    }
    }
}