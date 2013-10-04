using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  public abstract class RequestControlBuilder<TFilter, TOut> : IRequestControlBuilder<TOut> where TFilter : IRequestFilter
  {
    protected IList<TFilter> RequestFilterList { get; set; }
    protected IList<IControlComponent> ControlComponents { get; set; }

    protected RequestControlBuilder() {
      this.RequestFilterList = new List<TFilter>();
      this.ControlComponents = new List<IControlComponent>();
    }

    /// <summary>
    /// Adds any number of <see cref="ISearchRequestFilter"/> filters to the resulting <see cref="SearchControl"/>.
    /// </summary>
    /// <param name="filters">A params array of <see cref="ISearchRequestFilter"/>.</param>
    /// <returns>Itself as a <see cref="IReturnedAttributesReturnedLanguagesConfigureReferences"/>.</returns>
    public IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params TFilter[] filters) {
      if (filters != null) this.RequestFilterList = new List<TFilter>(filters);

      return this;
    }

    /// <summary>
    /// Configures how the api should handle attribute- and special character references in attribute data.
    /// </summary>
    /// <param name="referenceOptions">A params array of <see cref="IReferenceOptions"/> whose contents configure reference handling.</param>
    public virtual void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions) {
      if (referenceOptions != null) {
        this.ControlComponents.Add(new ReferenceControl(referenceOptions));
      }
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
          }
          else {
            AttributeControl control =
              this.ControlComponents.Where(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)
                  .Cast<AttributeControl>().Single();

            control.AddAttributes(attributeControl);
          }
        }
      }

      return this;
    }

    /// <summary>
    /// Used to restrict which attributes get returned by the API.
    /// </summary>
    /// <param name="definitionIds">A params array of attribute definition ids defining which attributes get returned.</param>
    /// <returns>Itself as a <see cref="IReturnedLanguagesConfigureReferences"/>.</returns>
    public IReturnedLanguagesConfigureReferences ReturnAttributes(params int[] definitionIds) {
      if (definitionIds == null || definitionIds.Length <= 0) return this;

      const string nodeName = "AttributesToReturn";
      if (!this.ControlComponents.Any(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)) {
        this.ControlComponents.Add(new AttributeControl(nodeName, definitionIds));
      }
      else {
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

    public IReturnedLanguagesConfigureReferences ReturnAttributes(params string[] attributeNames) {
      if (attributeNames == null || attributeNames.Length <= 0) return this;

      const string nodeName = "AttributesToReturn";
      if (!this.ControlComponents.Any(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)) {
        this.ControlComponents.Add(new AttributeControl(nodeName, attributeNames));
      }
      else {
        AttributeControl control =
          this.ControlComponents.Where(cc => cc is AttributeControl && cc.ToAdsml().Name.ToString() == nodeName)
            .Cast<AttributeControl>().Single();

        string nameList =
          attributeNames
            .Aggregate((aggr, d) => aggr + "+" + d);

        control.OuterNodeAttributes.Add(new XAttribute("namelist", nameList));
      }

      return this;
    }

    public IReturnedLanguagesConfigureReferences ReturnAttributes(int[] definitionIds, string[] attributeNames)  {
      ReturnAttributes(definitionIds);
      ReturnAttributes(attributeNames);

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

    /// <summary>
    /// Used to restrict in which languages attribute data gets returned.
    /// </summary>
    /// <param name="languagesToReturn">A params array of languages ids for which attribute data should be returned.</param>
    /// <returns>Itself as a <see cref="IConfigureReferences"/>.</returns>
    public IConfigureReferences ReturnLanguages(params int[] languagesToReturn) {
      IReturnedLanguageControl[] ltr =
        languagesToReturn
          .Select(LanguageToReturn.WithLanguageId)
          .Cast<IReturnedLanguageControl>()
          .ToArray();

      return ReturnLanguages(ltr);
    }

    public abstract TOut Build();
  }
}