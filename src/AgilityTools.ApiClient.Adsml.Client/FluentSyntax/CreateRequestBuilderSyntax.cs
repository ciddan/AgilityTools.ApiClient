using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Requests;

namespace AgilityTools.ApiClient.Adsml.Client.FluentSyntax
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ICreateRequestBuilder : IFluentInterface,
                                             INewContextName,
                                             IObjectTypeToCreate,
                                             IFailOnError,
                                             IAttributesToSet,
                                             IOTTCreateRNAttributesIFOErrorIATSet,
                                             IRNAttributesFOErrorIATSet,
                                             IFOErrorIATSet,
                                             IConfigLookupControls
    {
        CreateRequest Build();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IOTTCreateRNAttributesIFOErrorIATSet : IFluentInterface, IObjectTypeToCreate, IReturnNoAttributes, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IRNAttributesFOErrorIATSet : IFluentInterface, IReturnNoAttributes, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFOErrorIATSet : IFluentInterface, IFailOnError, IAttributesToSet { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface INewContextName : IFluentInterface
    {
        IOTTCreateRNAttributesIFOErrorIATSet NewContextName(string contextName);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IObjectTypeToCreate : IFluentInterface
    {
        IRNAttributesFOErrorIATSet ObjectTypeToCreate(string objectType);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnNoAttributes : IFluentInterface
    {
        IFOErrorIATSet ReturnNoAttributes();
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