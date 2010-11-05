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

        public IReturnedAttributeConfigureReferences RequestFilters(params ISearchRequestFilter[] filters) {
		    if (filters != null) {
		        this.RequestFilterList =
		            new List<ISearchRequestFilter>(filters);
		    }

            return this;
        }

        public IConfigureReferences ReturnedAttributes(params AttributeToReturn[] attributesToReturn)
        {
            if (attributesToReturn != null) {
                this.ControlComponents.Add(new AttributeSearchControl(attributesToReturn));
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