using System.Collections.Generic;
using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Requests;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ICreateRequestBuilder : IFluentInterface,
                                             INewContextName,
                                             IObjectTypeToCreate,
                                             IFailOnError,
                                             IAttributesToSet,
                                             IOTTCreateIFOErrorIATSet,
                                             IFOErrorIATSet,
                                             IConfigLookupControls
    {
        CreateRequest Build();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IOTTCreateIFOErrorIATSet : IFluentInterface, IObjectTypeToCreate, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFOErrorIATSet : IFluentInterface, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface INewContextName : IFluentInterface
    {
        IOTTCreateIFOErrorIATSet NewContextName(string contextName);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IObjectTypeToCreate : IFluentInterface
    {
        IFOErrorIATSet ObjectTypeToCreate(string objectType);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFailOnError : IFluentInterface
    {
        IAttributesToSet FailOnError();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAttributesToSet : IFluentInterface
    {
        IConfigLookupControls AttributesToSet(IList<StructureAttribute> structureAttributes);
        IConfigLookupControls AttributesToSet(params StructureAttribute[] structureAttributes);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConfigLookupControls
    {
        ILookupControlBuilder ConfigureLookupControls();
    }
}