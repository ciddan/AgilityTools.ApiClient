using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// Represents a ADSML SearchControl xml block. Usually created an configured by using the <see cref="SearchControlBuilder"/>.
  /// </summary>
  public class SearchControl : ControlBase
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="requestFilters">Optional. Any <see cref="ISearchControlFilter"/> used to configure the SearchControl.</param>
    /// <param name="components">Optional. Any <see cref="ISearchControlComponent"/> used to further configure the SearchControl.</param>
    internal SearchControl(IEnumerable<ISearchControlFilter> requestFilters, IEnumerable<ISearchControlComponent> components) {
      if (requestFilters != null) this.RequestFilters = new List<IRequestFilter>(requestFilters);
      if (components != null) this.Components = new List<IControlComponent>(components);

      this.OuterNodeAttributes = new List<XAttribute>();
      this.NodeName = "SearchControls";
    }
  }
}