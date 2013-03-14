using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Defines available commands and command order for the <see cref="ILookupControlBuilder"/>. 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ILookupControlBuilder : IFluentInterface,
                                           ILookupRequestFilters,
                                           IAttributeNamelist,
                                           IReturnedAttributesReturnedLanguagesConfigureReferences,
                                           IReturnedLanguagesConfigureReferences
  {
    LookupControl Build();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ILookupRequestFilters : IFluentInterface
  {
    IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ILookupControlFilter[] filters);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IAttributeNamelist : IFluentInterface
  {
    IReturnedAttributesReturnedLanguagesConfigureReferences AttributeNamelist(string attributeNames);
  }
}