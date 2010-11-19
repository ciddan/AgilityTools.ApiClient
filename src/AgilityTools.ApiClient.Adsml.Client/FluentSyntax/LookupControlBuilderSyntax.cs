using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Filters;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ILookupControlBuilder : IFluentInterface,
                                             ILookupRequestFilters,
                                             IReturnedAttributesReturnedLanguagesConfigureReferences,
                                             IReturnedLanguagesConfigureReferences
    {
        LookupControl Build();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ILookupRequestFilters : IFluentInterface
    {
        IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ILookupRequestFilter[] filters);
    }
}