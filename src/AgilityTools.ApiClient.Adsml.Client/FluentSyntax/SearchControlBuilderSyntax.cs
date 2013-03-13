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
                                           IReturnedAttributesReturnedLanguagesConfigureReferences,
                                           IReturnedLanguagesConfigureReferences
  {
    SearchControl Build();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedAttributesReturnedLanguagesConfigureReferences : IFluentInterface, IReturnedAttributes, IReturnedLanguages, IConfigureReferences { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedLanguagesConfigureReferences : IReturnedLanguages, IConfigureReferences { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ISearchControlFilters : IFluentInterface
  {
    IReturnedAttributesReturnedLanguagesConfigureReferences AddSearchControlFilters(params ISearchControlFilter[] filters);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedAttributes : IFluentInterface
  {
    IReturnedLanguagesConfigureReferences ReturnAttributes(params IAttributeControl[] attributeControls);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedLanguages : IFluentInterface
  {
    IConfigureReferences ReturnLanguages(params IReturnedLanguageControl[] languagesToReturn);
    IConfigureReferences ReturnLanguages(params int[] languagesToReturn);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IConfigureReferences : IFluentInterface
  {
    void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions);
  }
}