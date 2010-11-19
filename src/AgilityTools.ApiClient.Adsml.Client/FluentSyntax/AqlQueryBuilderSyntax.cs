using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAqlQueryBuilder : IFluentInterface, IBasePath, IQTypeIOTTFindIQStringICSControls, IOTTFindIQStringICSControls, IQStringICSControls
    {
        AqlSearchRequest Build();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IQTypeIOTTFindIQStringICSControls : IQueryType, IObjectTypeToFind, IQueryString, IConfigSearchControls { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IOTTFindIQStringICSControls : IObjectTypeToFind, IQueryString, IConfigSearchControls { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IQStringICSControls : IQueryString, IConfigSearchControls { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IBasePath : IFluentInterface
    {
        IQTypeIOTTFindIQStringICSControls BasePath(string path);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IQueryType : IFluentInterface
    {
        IOTTFindIQStringICSControls QueryType(AqlQueryTypes type);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IObjectTypeToFind : IFluentInterface
    {
        IQStringICSControls ObjectTypeToFind(int typeId);
        IQStringICSControls ObjectTypeToFind(string typeName);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IQueryString : IFluentInterface
    {
        IConfigSearchControls QueryString(string query);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConfigSearchControls
    {
        ISearchControlBuilder ConfigureSearchControls();
    }
}