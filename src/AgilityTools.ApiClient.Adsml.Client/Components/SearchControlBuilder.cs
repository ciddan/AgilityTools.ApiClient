using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// A builder class. Used to build and configure a <see cref="SearchControl"/>.
  /// </summary>
  public class SearchControlBuilder : RequestControlBuilder<ISearchControlFilter, SearchControl>, ISearchControlBuilder
  {
    /// <summary>
    /// Builds a <see cref="SearchControl"/> containing all defined parameters and options.
    /// </summary>
    /// <returns>A <see cref="SearchControl"/>.</returns>
    public override SearchControl Build() {
      return new SearchControl(this.RequestFilterList, this.ControlComponents.Cast<ISearchControlComponent>());
    }
  }
}