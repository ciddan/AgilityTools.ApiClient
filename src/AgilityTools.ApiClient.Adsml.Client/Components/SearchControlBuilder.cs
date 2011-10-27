using System.Collections.Generic;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// A builder class. Used to build and configure a <see cref="SearchControl"/>.
    /// </summary>
    public class SearchControlBuilder : ISearchControlBuilder
    {
        private IList<ISearchRequestFilter> RequestFilterList { get; set; }
        private IList<ISearchControlComponent> ControlComponents { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SearchControlBuilder() {
            ControlComponents = new List<ISearchControlComponent>();
        }

        /// <summary>
        /// Adds any number of <see cref="ISearchRequestFilter"/> filters to the resulting <see cref="SearchControl"/>.
        /// </summary>
        /// <param name="filters">A params array of <see cref="ISearchRequestFilter"/>.</param>
        /// <returns>Itself as a <see cref="IReturnedAttributesReturnedLanguagesConfigureReferences"/>.</returns>
        public IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ISearchRequestFilter[] filters) {
		    if (filters != null) {
		        this.RequestFilterList =
		            new List<ISearchRequestFilter>(filters);
		    }

            return this;
        }

        /// <summary>
        /// Used to restrict which attributes get returned by the API.
        /// </summary>
        /// <param name="attributesToReturn">A params array of <see cref="AttributeToReturn"/> defining which attributes get returned.</param>
        /// <returns>Itself as a <see cref="IReturnedLanguagesConfigureReferences"/>.</returns>
        public IReturnedLanguagesConfigureReferences ReturnAttributes(params AttributeToReturn[] attributesToReturn)
        {
            if (attributesToReturn != null) {
                this.ControlComponents.Add(new AttributeControl(attributesToReturn));
            }

            return this;
        }

        /// <summary>
        /// Used to restrict which attributes get returned by the API.
        /// </summary>
        /// <param name="attributesToReturn">A params array of <see cref="AttributeToReturn"/> defining which attributes get returned.</param>
        /// <returns>Itself as a <see cref="IReturnedLanguagesConfigureReferences"/>.</returns>
        public IReturnedLanguagesConfigureReferences ReturnAttributes(params int[] attributesToReturn) {
            AttributeToReturn[] atr = attributesToReturn.Select(AttributeToReturn.WithDefinitionId).ToArray();

            if (attributesToReturn != null) {
                this.ControlComponents.Add(new AttributeControl(atr));
            }

            return this;
        }

        /// <summary>
        /// Used to restrict in which languages attribute data gets returned.
        /// </summary>
        /// <param name="languagesToReturn">A params array of <see cref="IReturnedLanguageControl"/> containing which languages attribute data should be returned for.</param>
        /// <returns>Itself as a <see cref="IConfigureReferences"/>.</returns>
        public IConfigureReferences ReturnLanguages(params IReturnedLanguageControl[] languagesToReturn) {
            if (languagesToReturn != null) {
                this.ControlComponents.Add(new LanguageControl(languagesToReturn));
            }

            return this;
        }

        public IConfigureReferences ReturnLanguages(params int[] languagesToReturn) {
            LanguageToReturn[] ltr = languagesToReturn.Select(LanguageToReturn.WithLanguageId).ToArray();
            return ReturnLanguages(ltr);
        }

        /// <summary>
        /// Used to configure how the API handles different types of refernces (attributes, special characters, prices...).
        /// </summary>
        /// <param name="referenceOptions">A params array of <see cref="IReferenceOptions"/> defining how references should be handled.</param>
        public void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions) {
            if (referenceOptions != null) {
                this.ControlComponents.Add(new ReferenceControl(referenceOptions));
            }
        }

        /// <summary>
        /// Builds a <see cref="SearchControl"/> containing all defined parameters and options.
        /// </summary>
        /// <returns>A <see cref="SearchControl"/>.</returns>
        public SearchControl Build() {
            return new SearchControl(this.RequestFilterList, this.ControlComponents);
        }
    }
}