using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// A builder class used for building a <see cref="LookupControl"/>. Implements <see cref="ILookupControlBuilder"/>. Each function returns a
  /// different "view" of the object to structure and require a certain order when using the fluent api of the builder.
  /// </summary>
  public class LookupControlBuilder : RequestControlBuilder<ILookupControlFilter, LookupControl>, ILookupControlBuilder
  {
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
    /// Returns a <see cref="LookupControl"/> configured by the previous calls to the fluent api of the builder.
    /// </summary>
    /// <returns>A <see cref="LookupControl"/>.</returns>
    public override LookupControl Build() {
      return new LookupControl(this.RequestFilterList, this.ControlComponents.Cast<ILookupControlComponent>());
    }
  }
}