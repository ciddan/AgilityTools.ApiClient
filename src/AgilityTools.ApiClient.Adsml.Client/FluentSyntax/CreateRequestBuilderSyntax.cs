using System;
using System.Collections.Generic;
using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Defines available commands and command order for the <see cref="ICreateRequestBuilder"/>. 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ICreateRequestBuilder : IFluentInterface,
                       IRequestBuilder<CreateRequest>,
                       IParentIdPath,
                       INewContextName,
                       IObjectTypeToCreate,
                       IFailOnError,
                       IUpdateIfExists,
                       IAttributesToSet,
                       INewContextNameObjectTypeToCreateReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet,
                       IObjectTypeToCreateReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet,
                       IReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet,
                       IFailOnErrorUpdateIfExistsAttributesToSet,
                       IUpdateIfExistsAttributesToSet,
                       IConfigLookupControls
  {
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface INewContextNameObjectTypeToCreateReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet : IFluentInterface, INewContextName, IObjectTypeToCreate, IReturnNoAttributes, IFailOnError, IUpdateIfExists, IAttributesToSet { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IObjectTypeToCreateReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet : IFluentInterface, IObjectTypeToCreate, IReturnNoAttributes, IFailOnError, IUpdateIfExists, IAttributesToSet { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet : IFluentInterface, IReturnNoAttributes, IFailOnError, IUpdateIfExists, IAttributesToSet { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IFailOnErrorUpdateIfExistsAttributesToSet : IFluentInterface, IFailOnError, IUpdateIfExists, IAttributesToSet { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IUpdateIfExistsAttributesToSet : IFluentInterface, IUpdateIfExists, IAttributesToSet { }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IParentIdPath : IFluentInterface
  {
    INewContextNameObjectTypeToCreateReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet ParentIdPath(string parentIdPath);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface INewContextName : IFluentInterface
  {
    IObjectTypeToCreateReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet NewContextName(string contextName);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IObjectTypeToCreate : IFluentInterface
  {
    IReturnNoAttributesFailOnErrorUpdateIfExistsAttributesToSet ObjectTypeToCreate(string objectType);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IReturnNoAttributes : IFluentInterface
  {
    IFailOnErrorUpdateIfExistsAttributesToSet ReturnNoAttributes();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IFailOnError : IFluentInterface
  {
    IUpdateIfExistsAttributesToSet FailOnError();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IUpdateIfExists : IFluentInterface
  {
    IAttributesToSet UpdateIfExists();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IAttributesToSet : IFluentInterface
  {
    IConfigLookupControls AttributesToSet(IEnumerable<IAdsmlAttribute> structureAttributes);
    IConfigLookupControls AttributesToSet(params IAdsmlAttribute[] structureAttributes);
    IConfigLookupControls AttributesToSet(Func<IList<IAdsmlAttribute>> attributeListFactory);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IConfigLookupControls
  {
    ILookupControlBuilder ConfigureLookupControls();
  }
}