using System.ComponentModel;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISearchControlBuilder : IFluentInterface, ISearchRequestFilters, IReturnedAttributeConfigureReferences { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnedAttributeConfigureReferences : IFluentInterface, IReturnedAttributes, IConfigureReferences { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISearchRequestFilters : IFluentInterface
    {
        IReturnedAttributeConfigureReferences AddRequestFilters(params ISearchRequestFilter[] filters);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnedAttributes : IFluentInterface
    {
        IConfigureReferences SpecifyReturnedAttributes(params AttributeToReturn[] attributesToReturn);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConfigureReferences : IFluentInterface
    {
        void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions);
    }
}