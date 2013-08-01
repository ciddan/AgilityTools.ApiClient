using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Defines available commands and command order for the <see cref="AqlQueryBuilder"/>. 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IAqlQueryBuilder : IFluentInterface, IRequestBuilder<AqlSearchRequest>, IBasePath, IQTypeIOTTFindIQStringICSControls, IOTTFindIQStringICSControls, IQStringICSControls
  {
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IQTypeIOTTFindIQStringICSControls : IQueryType, IObjectTypeToFind, IQueryString, IConfigSearchControls, ISearchRequestFilters
  {
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IOTTFindIQStringICSControls : IObjectTypeToFind, IQueryString, IConfigSearchControls
  {
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IQStringICSControls : IQueryString, IConfigSearchControls
  {
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IBasePath : IFluentInterface
  {
    IQTypeIOTTFindIQStringICSControls BasePath(string path);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ISearchRequestFilters : IFluentInterface
  {
    IQTypeIOTTFindIQStringICSControls SearchRequestFilters(params ISearchRequestFilter[] filters);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IQueryType : IFluentInterface
  {
    IOTTFindIQStringICSControls QueryType(AqlQueryTypes type);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IObjectTypeToFind : IFluentInterface
  {
    IQStringICSControls ObjectTypeToFind(params int[] typeIds);
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