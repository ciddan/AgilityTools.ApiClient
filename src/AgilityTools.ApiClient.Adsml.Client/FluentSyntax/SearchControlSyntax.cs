using System.ComponentModel;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISearchControlBuilder : IFluentInterface, ISearchRequestFilters, IReturnedAttributes { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISearchRequestFilters : IFluentInterface
    {
        IReturnedAttributes RequestFilters(params ISearchRequestFilter[] filters);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnedAttributes : IFluentInterface
    {
        void ReturnedAttributes(params AttributeToReturn[] attributesToReturn);
    }
}