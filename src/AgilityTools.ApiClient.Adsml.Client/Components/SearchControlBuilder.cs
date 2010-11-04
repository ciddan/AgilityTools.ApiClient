using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class SearchControlBuilder : ISearchControlBuilder
    {
        private IList<ISearchRequestFilter> RequestFilterList { get; set; }
        private IList<AttributeToReturn> AttributesToReturn { get; set; }

        public IReturnedAttributes RequestFilters(params ISearchRequestFilter[] filters) {
		    if (filters != null) {
		        this.RequestFilterList =
		            new List<ISearchRequestFilter>(filters);
		    }

            return this;
        }

        public void ReturnedAttributes(params AttributeToReturn[] attributesToReturn)
        {
            if (attributesToReturn != null) {
                this.AttributesToReturn =
                    new List<AttributeToReturn>(attributesToReturn);
            }
        }

        public SearchControl Build()
        {
            return new SearchControl(this.RequestFilterList, this.AttributesToReturn);
        }
    }
}