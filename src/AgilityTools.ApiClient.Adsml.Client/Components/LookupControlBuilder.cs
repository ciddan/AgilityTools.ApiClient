using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class LookupControlBuilder : ILookupControlBuilder
    {
        private IList<ILookupRequestFilter> RequestFilterList { get; set; }
        private IList<ILookupControlComponent> ControlComponents { get; set; }

        public LookupControlBuilder() {
            ControlComponents = new List<ILookupControlComponent>();
        }

        public IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ILookupRequestFilter[] filters) {
            if (filters != null) {
                this.RequestFilterList =
                    new List<ILookupRequestFilter>(filters);
            }

            return this;
        }

        public IReturnedLanguagesConfigureReferences ReturnAttributes(params IAttributeControl[] attributesToReturn) {
            if (attributesToReturn != null) {
                this.ControlComponents.Add(new AttributeControl(attributesToReturn));
            }

            return this;
        }

        public IConfigureReferences ReturnLanguages(params IReturnedLanguageControl[] languagesToReturn) {
            if (languagesToReturn != null) {
                this.ControlComponents.Add(new LanguageControl(languagesToReturn));
            }

            return this;
        }

        public void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions) {
            if (referenceOptions != null) {
                this.ControlComponents.Add(new ReferenceControl(referenceOptions));
            }
        }

        public LookupControl Build() {
            return new LookupControl(this.RequestFilterList, this.ControlComponents);
        }
    }
}