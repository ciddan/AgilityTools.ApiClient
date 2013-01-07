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
                                             IParentIdPath,
                                             INewContextName,
                                             IObjectTypeToCreate,
                                             IFailOnError,
                                             IAttributesToSet,
                                             INewContextNameObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet,
                                             IObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet,
                                             IReturnNoAttributesFailOnErrorAttributesToSet,
                                             IFailOnErrorAttributesToSet,
                                             IConfigLookupControls
    {
        CreateRequest Build();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface INewContextNameObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet : IFluentInterface, INewContextName, IObjectTypeToCreate, IReturnNoAttributes, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet : IFluentInterface, IObjectTypeToCreate, IReturnNoAttributes, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnNoAttributesFailOnErrorAttributesToSet : IFluentInterface, IReturnNoAttributes, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFailOnErrorAttributesToSet : IFluentInterface, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IParentIdPath : IFluentInterface
    {
        INewContextNameObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet ParentIdPath(string parentIdPath);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface INewContextName : IFluentInterface
    {
        IObjectTypeToCreateReturnNoAttributesFailOnErrorAttributesToSet NewContextName(string contextName);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IObjectTypeToCreate : IFluentInterface
    {
        IReturnNoAttributesFailOnErrorAttributesToSet ObjectTypeToCreate(string objectType);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnNoAttributes : IFluentInterface
    {
        IFailOnErrorAttributesToSet ReturnNoAttributes();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFailOnError : IFluentInterface
    {
        IAttributesToSet FailOnError();
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