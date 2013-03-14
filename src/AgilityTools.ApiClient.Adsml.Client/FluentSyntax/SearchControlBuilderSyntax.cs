using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Defines available commands and command order for the <see cref="ISearchControlBuilder"/>. 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ISearchControlBuilder : IFluentInterface,
                                           ISearchControlFilters,
                                           IRequestControlBuilder<SearchControl>
  {
    SearchControl Build();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ISearchControlFilters : IFluentInterface
  {
    IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ISearchControlFilter[] filters);
  }
}