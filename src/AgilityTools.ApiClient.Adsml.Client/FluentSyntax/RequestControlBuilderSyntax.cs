using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public interface IRequestControlBuilder<T> : IFluentInterface,
                                               IReturnedAttributesReturnedLanguagesConfigureReferences,
                                               IReturnedLanguagesConfigureReferences
  {
    T Build();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedAttributes : IFluentInterface
  {
    IReturnedLanguagesConfigureReferences ReturnAttributes(params IAttributeControl[] attributeControls);
    IReturnedLanguagesConfigureReferences ReturnAttributes(params int[] definitionIds);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IConfigureReferences : IFluentInterface
  {
    void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedLanguages : IFluentInterface
  {
    IConfigureReferences ReturnLanguages(params IReturnedLanguageControl[] languagesToReturn);
    IConfigureReferences ReturnLanguages(params int[] languagesToReturn);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedAttributesReturnedLanguagesConfigureReferences : IFluentInterface, IReturnedAttributes, IReturnedLanguages, IConfigureReferences { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnedLanguagesConfigureReferences : IReturnedLanguages, IConfigureReferences { }
}