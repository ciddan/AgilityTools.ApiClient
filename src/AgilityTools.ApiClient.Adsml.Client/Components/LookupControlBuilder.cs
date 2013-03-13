using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// A builder class used for building a <see cref="LookupControl"/>. Implements <see cref="ILookupControlBuilder"/>. Each function returns a
  /// different "view" of the object to structure and require a certain order when using the fluent api of the builder.
  /// </summary>
  public class LookupControlBuilder : ILookupControlBuilder
  {
    private IList<ILookupControlFilter> RequestFilterList { get; set; }
    private IList<ILookupControlComponent> ControlComponents { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LookupControlBuilder() {
      ControlComponents = new List<ILookupControlComponent>();
    }

    /// <summary>
    /// Adds any number of <see cref="ILookupControlFilter"/>s to the <see cref="LookupControl"/>.
    /// </summary>
    /// <param name="filters">A params array of <see cref="ILookupControlFilter"/>. All filters get added to the resulting <see cref="LookupControl"/>.</param>
    /// <returns>Itself as a <see cref="IReturnedAttributesReturnedLanguagesConfigureReferences"/></returns>
    public IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ILookupControlFilter[] filters) {
      if (filters != null) {
        this.RequestFilterList =
          new List<ILookupControlFilter>(filters);
      }

      return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attributeNames"></param>
    /// <returns>Itself as a <see cref="IReturnedAttributesReturnedLanguagesConfigureReferences"/></returns>
    public IReturnedAttributesReturnedLanguagesConfigureReferences AttributeNamelist(string attributeNames) {
      if (!string.IsNullOrEmpty(attributeNames)) {
        var aControl = new AttributeControl("AttributesToReturn") {
          OuterNodeAttributes = new List<XAttribute> {
            new XAttribute("namelist", attributeNames)
          }
        };

        this.ControlComponents.Add(aControl);
      }

      return this;
    }
    
    /// <summary>
    /// Adds any number of <see cref="AttributeToReturn"/> tags to the resulting <see cref="LookupControl"/>. Used to limit which attributes get returned.
    /// </summary>
    /// <param name="attributeControls">A param array of <see cref="AttributeToReturn"/> containing the attributes that should be returned.</param>
    /// <returns>Itself as a <see cref="IReturnedLanguagesConfigureReferences"/></returns>
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
    /// Restricts in which languages data gets returned.
    /// </summary>
    /// <param name="languagesToReturn">A params array of <see cref="IReturnedLanguageControl"/> containing the languages to return data in.</param>
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
    /// Configures how the api should handle attribute- and special character references in attribute data.
    /// </summary>
    /// <param name="referenceOptions">A params array of <see cref="IReferenceOptions"/> whose contents configure reference handling.</param>
    public void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions) {
      if (referenceOptions != null) {
        this.ControlComponents.Add(new ReferenceControl(referenceOptions));
      }
    }

    /// <summary>
    /// Returns a <see cref="LookupControl"/> configured by the previous calls to the fluent api of the builder.
    /// </summary>
    /// <returns>A <see cref="LookupControl"/>.</returns>
    public LookupControl Build() {
      return new LookupControl(this.RequestFilterList, this.ControlComponents);
    }
  }
}