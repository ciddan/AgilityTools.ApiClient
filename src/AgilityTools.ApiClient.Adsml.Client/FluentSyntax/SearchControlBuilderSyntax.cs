using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Defines available commands and command order for the <see cref="ISearchControlBuilder"/>. 
    /// </summary>
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
        IReturnedLanguagesConfigureReferences ReturnAttributes(params AttributeToReturn[] attributesToReturn);
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