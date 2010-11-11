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
                                             IFOErrorIATSet
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
        void AttributesToSet(IList<StructureAttribute> structureAttributes);
        void AttributesToSet(params StructureAttribute[] structureAttributes);
    }
}