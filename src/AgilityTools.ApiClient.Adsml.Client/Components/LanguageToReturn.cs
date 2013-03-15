using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Implementation of <see cref="IReturnedLanguageControl"/>.
    /// </summary>
    public class LanguageToReturn : IReturnedLanguageControl
    {
        private readonly int _langId;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="langId">Required. The id of the language to return.</param>
        internal LanguageToReturn(int langId) {
            _langId = langId;
        }

        /// <summary>
        /// Serialized the object into ADSML xml form.
        /// </summary>
        /// <returns><see cref="XElement"/></returns>
        public XElement ToAdsml() {
            this.Validate();

            return new XElement("Language", new XAttribute("id", _langId.ToString()));
        }

        /// <summary>
        /// Validates the object state.
        /// </summary>
        /// <exception cref="ApiSerializationValidationException">Thrown if the language id has not been set, or is 0.</exception>
        public void Validate() {
            if (_langId <= 0) {
                throw new ApiSerializationValidationException("LanguageId cannot be negative or 0.");
            }
        }

        /// <summary>
        /// A Factory method used to create a new instance of a <see cref="LanguageToReturn"/> with the provided language id set.
        /// </summary>
        /// <param name="langId">Required. The language id of the language to return.</param>
        /// <returns><see cref="LanguageToReturn"/>.</returns>
        public static LanguageToReturn WithLanguageId(int langId) {
            return new LanguageToReturn(langId);
        }
    }
}