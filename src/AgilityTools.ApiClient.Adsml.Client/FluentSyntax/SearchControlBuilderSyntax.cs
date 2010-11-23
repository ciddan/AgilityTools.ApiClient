using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Filters;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISearchControlBuilder : IFluentInterface, 
                                             ISearchRequestFilters, 
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
    public interface ISearchRequestFilters : IFluentInterface
    {
        IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ISearchRequestFilter[] filters);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnedAttributes : IFluentInterface
    {
        IReturnedLanguagesConfigureReferences ReturnAttributes(params IAttributeControl[] attributesToReturn);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnedLanguages : IFluentInterface
    {
        IConfigureReferences ReturnLanguages(params IReturnedLanguageControl[] languagesToReturn);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConfigureReferences : IFluentInterface
    {
        void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions);
    }
}