using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class LanguageToReturn : IReturnedLanguageControl
    {
        private readonly int _langId;

        internal LanguageToReturn(int langId) {
            _langId = langId;
        }

        public XElement ToAdsml() {
            this.Validate();

            return new XElement("Language", new XAttribute("id", _langId.ToString()));
        }

        public void Validate() {
            if (_langId <= 0) {
                throw new ApiSerializationValidationException("LanguageId cannot be negative or 0.");
            }
        }

        public static LanguageToReturn WithLanguageId(int langId) {
            return new LanguageToReturn(langId);
        }
    }
}