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
                                             ISearchRequestFilters, 
                                             IReturnedAttributesReturnedLanguagesConfigureReferences, 
                                             IReturnedLanguagesConfigureReferences
    {
        SearchControl Build();

        /// <summary>
        /// Used to restrict which attributes get returned by the API.
        /// </summary>
        /// <param name="attributesToReturn">A params array of <see cref="AttributeToReturn"/> defining which attributes get returned.</param>
        /// <returns>Itself as a <see cref="IReturnedLanguagesConfigureReferences"/>.</returns>
        IReturnedLanguagesConfigureReferences ReturnAttributes(params int[] attributesToReturn);
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
        IReturnedLanguagesConfigureReferences ReturnAttributes(params int[] attributesToReturn);
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