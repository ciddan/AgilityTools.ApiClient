using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// A builder class. Used to build and configure a <see cref="SearchControl"/>.
  /// </summary>
  public class SearchControlBuilder : ISearchControlBuilder
  {
    private IList<ISearchControlFilter> RequestFilterList { get; set; }
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
    public IReturnedAttributesReturnedLanguagesConfigureReferences AddSearchControlFilters(params ISearchControlFilter[] filters) {
      if (filters != null) this.RequestFilterList = new List<ISearchControlFilter>(filters);

      return this;
    }


    /// <summary>
    /// Used to restrict which attributes get returned by the API.
    /// </summary>
    /// <param name="attributeControls">A params array of <see cref="AttributeToReturn"/> defining which attributes get returned.</param>
    /// <returns>Itself as a <see cref="IReturnedLanguagesConfigureReferences"/>.</returns>
    public IReturnedLanguagesConfigureReferences ReturnAttributes(params IAttributeControl[] attributeControls) {
      if (attributeControls != null && attributeControls.Length > 0) {
        foreach (IAttributeControl attributeControl in attributeControls) {
          string nodeName = attributeControl.OuterNodeName;

          if (!this.ControlComponents.Any(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)) {
            this.ControlComponents.Add(new AttributeControl(nodeName, attributeControl));
          } else {
            AttributeControl control =
              this.ControlComponents.Where(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)
                  .Cast<AttributeControl>().Single();

            control.AddAttributes(attributeControl);
          }
        }
      }

      return this;
    }

    public IReturnedLanguagesConfigureReferences ReturnAttributes(params int[] definitionIds) {
      if (definitionIds == null || definitionIds.Length <= 0) return this;

      const string nodeName = "AttributesToReturn";
      if (!this.ControlComponents.Any(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)) {
        this.ControlComponents.Add(new AttributeControl(nodeName, definitionIds));
      } else {
        AttributeControl control =
          this.ControlComponents.Where(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)
              .Cast<AttributeControl>().Single();

        string idList =
          definitionIds
            .Select(d => d.ToString(CultureInfo.InvariantCulture))
            .Aggregate((aggr, d) => aggr + ", " + d);

        control.OuterNodeAttributes.Add(new XAttribute("idlist", idList));
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