using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class SearchControlBuilder : ISearchControlBuilder
    {
        private IList<ISearchRequestFilter> RequestFilterList { get; set; }
        private IList<ISearchControlComponent> ControlComponents { get; set; }

        public SearchControlBuilder() {
            ControlComponents = new List<ISearchControlComponent>();
        }

        public IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ISearchRequestFilter[] filters) {
		    if (filters != null) {
		        this.RequestFilterList =
		            new List<ISearchRequestFilter>(filters);
		    }

            return this;
        }

        public IReturnedLanguagesConfigureReferences ReturnAttributes(params IAttributeControl[] attributesToReturn)
        {
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

        public SearchControl Build()
        {
            return new SearchControl(this.RequestFilterList, this.ControlComponents);
        }
    }
}